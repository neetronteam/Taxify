using Taxify.Domain.Entities;

namespace Taxify.Service.DTOs.Vehicles;

public class VehicleCreationDto
{
    public long DriverId { get; set; }
    public long CarId { get; set; }
}
