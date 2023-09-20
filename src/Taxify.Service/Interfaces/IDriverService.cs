using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Drivers;

namespace Taxify.Service.Interfaces;

public interface IDriverService
{
    ValueTask<DriverResultDto> AddAsync(DriverCreationDto dto);
    ValueTask<DriverResultDto> ModifyAsync(DriverUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<DriverResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<DriverResultDto>> RetrieveAllAsync(PaginationParams @params);
}