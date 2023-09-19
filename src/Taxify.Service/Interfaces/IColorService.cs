using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Colors;

namespace Taxify.Service.Interfaces;

public interface IColorService
{
    ValueTask<ColorResultDto> AddAsync(ColorCreationDto dto);
    ValueTask<ColorResultDto> ModifyAsync(ColorUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<ColorResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<ColorResultDto>> RetrieveAllAsync(PaginationParams @params);
}