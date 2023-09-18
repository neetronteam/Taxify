using Taxify.Service.DTOs.Drive;
using Taxify.Service.DTOs.Users;

namespace Taxify.Service.DTOs.Orders;

public class OrderResultDto
{
    public long Id { get; set; }
    public short NumberOfPassenger { get; set; }
    public UserResultDto User { get; set; }
    public DriveResultDto Drive { get; set; }
}
