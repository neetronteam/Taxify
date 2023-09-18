using Taxify.Service.DTOs.Cars;
using Taxify.Service.DTOs.Drivers;

namespace Taxify.Service.DTOs.Vehicles;

public class VehicleResultDto
{
    public long Id { get; set; }
    public DriverResultDto Driver { get; set; }
    public CarResultDto Car { get; set; }
}