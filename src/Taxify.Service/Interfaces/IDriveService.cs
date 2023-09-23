using Taxify.Domain.Configuration;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Drive;

namespace Taxify.Service.Interfaces;

public interface IDriveService
{
    ValueTask<DriveResultDto> AddAsync(DriveCreationDto dto);
    ValueTask<DriveResultDto> ModifyAsync(DriveUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<bool> DestroyAsync(long id);
    ValueTask<DriveResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<DriveResultDto>> RetrieveAllAsync(PaginationParams @params);
    ValueTask<IEnumerable<DriveResultDto>> FilterByDepartureAsync(string departure, PaginationParams @params);
    ValueTask<IEnumerable<DriveResultDto>> FilterByDestinationAsync(string destination, PaginationParams @params);
    ValueTask<IEnumerable<DriveResultDto>> FilterByDepartureAndDestination(string departure, string destination,
        PaginationParams @params);
}