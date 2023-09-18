using Taxify.Domain.Commons;

namespace Taxify.Domain.Entities;

public class Order : Auditable
{
    public short NumberOfPassenger { get; set; }
    
    public long UserId { get; set; }
    public User User { get; set; }

    public long DriverId { get; set; }
    public Drive Drive { get; set; }
}
