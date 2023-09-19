using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.CarModels;

namespace Taxify.Service.Interfaces;

public interface ICarModelService
{
    ValueTask<CarModelResultDto> AddAsync(CarModelCreationDto dto);
    ValueTask<CarModelResultDto> ModifyAsync(CarModelUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<bool> DestroyAsync(long id);
    ValueTask<CarModelResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<CarModelResultDto>> RetrieveAllAsync(PaginationParams @params);
}