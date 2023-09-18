namespace Taxify.Service.DTOs.Orders;

public class OrderUpdateDto
{
    public long Id { get; set; }
    public short NumberOfPassenger { get; set; }
    public long UserId { get; set; }
    public long DriverId { get; set; }
}
