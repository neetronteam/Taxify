using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Vehicles;

namespace Taxify.Service.Interfaces;

public interface IVehicleService
{
    ValueTask<VehicleResultDto> AddAsync(VehicleCreationDto dto);
    ValueTask<VehicleResultDto> ModifyAsync(VehicleUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<VehicleResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<VehicleResultDto>> RetrieveAllAsync(PaginationParams @params);
}