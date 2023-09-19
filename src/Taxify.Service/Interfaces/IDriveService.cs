using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Drive;

namespace Taxify.Service.Interfaces;

public interface IDriveService
{
    ValueTask<DriveResultDto> AddAsync(DriveCreationDto dto);
    ValueTask<DriveResultDto> ModifyAsync(DriveUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<DriveResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<DriveResultDto>> RetrieveAllAsync(PaginationParams @params);
}