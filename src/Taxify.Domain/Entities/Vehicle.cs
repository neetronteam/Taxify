using Taxify.Domain.Commons;

namespace Taxify.Domain.Entities;

public class Vehicle : Auditable
{
    public long DriverId { get; set; }
    public Driver Driver { get; set; }

    public long CarId { get; set; }
    public Car Car { get; set; }    
}
