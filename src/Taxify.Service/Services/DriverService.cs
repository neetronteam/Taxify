using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Drivers;
using Taxify.Service.Interfaces;

namespace Taxify.Service.Services;

public class DriverService : IDriverService
{
    public ValueTask<DriverResultDto> AddAsync(DriverCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> DestroyAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<DriverResultDto> ModifyAsync(DriverUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> RemoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<DriverResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public ValueTask<DriverResultDto> RetrieveByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}