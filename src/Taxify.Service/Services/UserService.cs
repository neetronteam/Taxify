using AutoMapper;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;
using Taxify.Domain.Configuration;
using Taxify.DataAccess.Contracts;
using Taxify.Service.DTOs.Attachments;
using System.ComponentModel.DataAnnotations;

namespace Taxify.Service.Services;

public class UserService : IUserService
{
    private readonly IAttachmentService attachmentService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public UserService(IMapper mapper, IUnitOfWork unitOfWork, IAttachmentService attachmentService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.attachmentService = attachmentService;
    }
    public async ValueTask<UserResultDto> RegisterAsync(UserCreationDto dto)
    {
        var existUser = await this.unitOfWork.UserRepository.SelectAsync(x => x.Phone.Equals(dto.Phone) && x.IsDeleted.Equals(false));
        if (existUser is not null)
            throw new AlreadyExistsException("This user already exist!");

        dto.Password = dto.Password.Hash();
        var mappedUser = this.mapper.Map<User>(dto);

        await this.unitOfWork.UserRepository.CreateAsync(mappedUser);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<UserResultDto>(mappedUser);
    }

    public async ValueTask<bool> DestroyAsync(long id)
    {
        var user = await this.unitOfWork.UserRepository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("This user not found!");

        this.unitOfWork.UserRepository.Destroy(user);
        await this.unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<UserResultDto> LoginAsync(UserLoginDto dto)
    {
        var user = await this.unitOfWork.UserRepository.SelectAsync(x => x.Phone.Equals(dto.Phone) && x.IsDeleted.Equals(false), includes: new[] {"Attachment"})
                         ?? throw new NotFoundException("This user is not found!");
        
        var isCorrect = PasswordHasher.Verify(dto.Password,user.Password);
        if(isCorrect is false)
            throw new WrongPasswordException("Password is not correct!");
        
        return this.mapper.Map<UserResultDto>(user);
    }

    public async ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        var user = await this.unitOfWork.UserRepository.SelectAsync(x => x.Id.Equals(dto.Id))
                     ?? throw new NotFoundException("This user not found!");

        this.mapper.Map(dto,user);

        this.unitOfWork.UserRepository.Update(user);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<UserResultDto>(user);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var user = await this.unitOfWork.UserRepository.SelectAsync(x => x.Id.Equals(id))
                              ?? throw new NotFoundException("This user not found!");

        this.unitOfWork.UserRepository.Delete(user);
        await this.unitOfWork.SaveAsync();

        return true;
    }

    public IEnumerable<UserResultDto> RetrieveAllAsync(PaginationParams @params)
    {
        var users = this.unitOfWork.UserRepository.SelectAll()
                                                  .ToPaginate(@params);

        return this.mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public async ValueTask<UserResultDto> RetrieveByIdAsync(long id)
    {
        var user = await this.unitOfWork.UserRepository.SelectAsync(x => x.Id.Equals(id) && x.IsDeleted.Equals(false),includes: new[] {"Attachment"})
                                        ?? throw new NotFoundException("User is not found!");

        var attachementResultDto = await this.attachmentService.RetriveByIdAsync(user.AttachmentId);
       
        var result = this.mapper.Map<UserResultDto>(user);
        result.Attachment = attachementResultDto;

        return this.mapper.Map<UserResultDto>(user);
    }

    public async ValueTask<UserResultDto> RetrieveByPhoneAsync(string phone)
    {
        var user = await this.unitOfWork.UserRepository.SelectAsync(x => x.Phone.Equals(phone) && x.IsDeleted.Equals(false))
                                        ?? throw new NotFoundException("User is not found!");
        
        var attachementResultDto = await this.attachmentService.RetriveByIdAsync(user.AttachmentId);
       
        var result = this.mapper.Map<UserResultDto>(user);
        result.Attachment = attachementResultDto;

        return result;
    }

    public async ValueTask<bool> UpdatePasswordAsync(long userId, string oldPassword, string newPassword)
    {
        var user = await this.unitOfWork.UserRepository.SelectAsync(x => x.Id.Equals(userId))
                                         ?? throw new NotFoundException("User is not found!");

        var isVerify = PasswordHasher.Verify(oldPassword,user.Password);
        if(isVerify is false)
            throw new WrongPasswordException("Password is not correct!");

        user.Password = newPassword.Hash();
        await this.unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<UserResultDto> UploadImageAsync(long userId, AttachmentCreationDto dto)
    { 
        var user = await this.unitOfWork.UserRepository.SelectAsync(x=> x.Id.Equals(userId) && x.IsDeleted.Equals(false))
                                         ?? throw new NotFoundException("This user is not found!");
    
        var createdAttachment = await this.attachmentService.UploadAsync(dto);
        user.AttachmentId = createdAttachment.Id;
        user.Attachment = createdAttachment;
        this.unitOfWork.UserRepository.Update(user);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<UserResultDto>(user);
    }

    public async ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync()
    {        
        var users = this.unitOfWork.UserRepository.SelectAll(includes: new[] {"Attachment"} );

        return mapper.Map<List<UserResultDto>>(users);
    }
}