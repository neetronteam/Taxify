using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taxify.DataAccess.Contracts;
using Taxify.Domain.Configuration;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Orders;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;

namespace Taxify.Service.Services;

public class OrderService:IOrderService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<OrderResultDto> AddAsync(OrderCreationDto dto)
    {
        var existUser = await _unitOfWork.UserRepository
                    .SelectAsync(expression: user => user.Id == dto.UserId)
                    ?? throw new NotFoundException(message: "User is not found");

        var existDrive = await _unitOfWork.DriveRepository
                        .SelectAsync(expression: drive => drive.Id == dto.DriveId)
                        ?? throw new NotFoundException(message: "Drive is not found");

        var order = _mapper.Map<Order>(source: dto);
        order.User = existUser;
        order.Drive = existDrive;

        await _unitOfWork.OrderRepository.CreateAsync(entity: order);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<OrderResultDto>(source: order);
    }

    public async ValueTask<OrderResultDto> ModifyAsync(OrderUpdateDto dto)
    {
        var existOrder = await _unitOfWork.OrderRepository
                        .SelectAsync(expression: order => order.Id == dto.Id, includes: new[] { "User", "Drive" })
                        ?? throw new NotFoundException(message: "Order is not found");

        var mappedOrder = _mapper.Map(source: dto, destination: existOrder);
        
        _unitOfWork.OrderRepository.Update(entity:mappedOrder);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<OrderResultDto>(source: mappedOrder);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var existOrder = await _unitOfWork.OrderRepository
                        .SelectAsync(expression: order => order.Id == id, includes: new[] { "User", "Drive" })
                        ?? throw new NotFoundException(message: "Order is not found");
        
        _unitOfWork.OrderRepository.Delete(entity:existOrder);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<bool> DestroyAsync(long id)
    {
        var existOrder = await _unitOfWork.OrderRepository
                        .SelectAsync(expression: order => order.Id == id, includes: new[] { "User", "Drive" })
                        ?? throw new NotFoundException(message: "Order is not found");
        
        _unitOfWork.OrderRepository.Destroy(entity:existOrder);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<OrderResultDto> RetrieveByIdAsync(long id)
    {
        var existOrder = await _unitOfWork.OrderRepository
                        .SelectAsync(expression: order => order.Id == id, includes: new[] { "User", "Drive" })
                        ?? throw new NotFoundException(message: "Order is not found");

        return _mapper.Map<OrderResultDto>(source: existOrder);
    }

    public async ValueTask<IEnumerable<OrderResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var orders = await _unitOfWork.OrderRepository
            .SelectAll()
            .ToPaginate(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<OrderResultDto>>(source: orders);
    }
}