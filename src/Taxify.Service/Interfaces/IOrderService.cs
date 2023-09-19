using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Orders;

namespace Taxify.Service.Interfaces;

public interface IOrderService
{
    ValueTask<OrderResultDto> AddAsync(OrderCreationDto dto);
    ValueTask<OrderResultDto> ModifyAsync(OrderUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<OrderResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<OrderResultDto>> RetrieveAllAsync(PaginationParams @params);
}