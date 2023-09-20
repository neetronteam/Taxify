using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taxify.DataAccess.Contracts;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Messages;
using Taxify.Service.Exceptions;
using Taxify.Service.Interfaces;

namespace Taxify.Service.Services;

public class MessageService : IMessageService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<MessageResultDto> AddAsync(MessageCreationDto dto)
    {
        var sender = await _unitOfWork.UserRepository
                    .SelectAsync(expression: user => user.Id == dto.SenderId)
                     ?? throw new NotFoundException(message: "Sender is not found");

        var receiver = await _unitOfWork.UserRepository
                      .SelectAsync(expression: user => user.Id == dto.ReceiveId)
                       ?? throw new NotFoundException(message: "Receiver is not found");

        var message = _mapper.Map<Message>(source: dto);
        message.Sender = sender;
        message.Receiver = receiver;

        await _unitOfWork.MessageRepository.CreateAsync(entity: message);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<MessageResultDto>(source: message);
    }

    public async ValueTask<MessageResultDto> ModifyAsync(MessageUpdateDto dto)
    {
        var message = await _unitOfWork.MessageRepository
                     .SelectAsync(expression: message => message.Id == dto.Id)
                      ?? throw new NotFoundException(message: "Message is not found");

        _mapper.Map(source: dto, destination: message);
        _unitOfWork.MessageRepository.Update(entity: message);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<MessageResultDto>(source: message);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var message = await _unitOfWork.MessageRepository
                     .SelectAsync(expression: message => message.Id == id)
                      ?? throw new NotFoundException(message: "Message is not found");

        _unitOfWork.MessageRepository.Delete(entity: message);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<bool> DestroyAsync(long id)
    {
        var message = await _unitOfWork.MessageRepository
                      .SelectAsync(expression: message => message.Id == id)
                      ?? throw new NotFoundException(message: "Message is not found");

        _unitOfWork.MessageRepository.Destroy(entity: message);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<MessageResultDto> RetrieveByIdAsync(long id)
    {
        var message = await _unitOfWork.MessageRepository
                      .SelectAsync(expression: message => message.Id == id)
                      ?? throw new NotFoundException(message: "Message is not found");

        return _mapper.Map<MessageResultDto>(source: message);
    }

    public async ValueTask<IEnumerable<MessageResultDto>> RetrieveAllAsync()
    {
        var messages = await _unitOfWork.MessageRepository
                        .SelectAll()
                        .ToListAsync();

        return _mapper.Map<IEnumerable<MessageResultDto>>(source: messages);
    }
}