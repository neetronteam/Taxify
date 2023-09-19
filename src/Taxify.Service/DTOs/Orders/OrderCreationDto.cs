using Taxify.Domain.Entities;

namespace Taxify.Service.DTOs.Orders;

public class OrderCreationDto
{
    public short NumberOfPassenger { get; set; }
    public long UserId { get; set; }
    public long DriveId { get; set; }
}
