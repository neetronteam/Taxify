using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Colors;

namespace Taxify.Service.Interfaces;

public interface IColorService
{
    ValueTask<ColorResultDto> AddAsync(ColorCreationDto dto);
    ValueTask<ColorResultDto> ModifyAsync(ColorUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<bool> DestroyAsync(long id);
    ValueTask<ColorResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<ColorResultDto>> RetrieveAllAsync(PaginationParams @params);
}