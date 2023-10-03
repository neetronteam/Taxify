using Taxify.Domain.Enums;
using Taxify.Service.DTOs.Users;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Attachments;

namespace Taxify.Service.Interfaces;

public interface IUserService
{
    ValueTask<UserResultDto> RegisterAsync(UserCreationDto dto);
    ValueTask<UserResultDto> LoginAsync(UserLoginDto dto);
    ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<bool> DestroyAsync(long id);
    ValueTask<UserResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync();
    ValueTask<UserResultDto> RetrieveByPhoneAsync(string phone);
    ValueTask<UserResultDto> UpgradeRoleAsync(long id, Role role);
    ValueTask<bool> UpdatePasswordAsync(long UserId,string oldPassword, string newPassword); 
    ValueTask<UserResultDto> UploadImageAsync(long userId,AttachmentCreationDto dto);
    IEnumerable<UserResultDto> RetrieveAllAsync(PaginationParams @params);
}