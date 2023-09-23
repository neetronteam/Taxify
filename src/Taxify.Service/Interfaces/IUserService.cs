using Taxify.Domain.Configuration;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Attachments;
using Taxify.Service.DTOs.Users;

namespace Taxify.Service.Interfaces;

public interface IUserService
{
    ValueTask<UserResultDto> AddAsync(UserCreationDto dto);
    ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<bool> DestroyAsync(long id);
    ValueTask<UserResultDto> RetrieveByIdAsync(long id);
    ValueTask<UserResultDto> UploadImageAsync(long userId,AttachmentCreationDto dto);
    IEnumerable<UserResultDto> RetrieveAllAsync(PaginationParams @params);
}