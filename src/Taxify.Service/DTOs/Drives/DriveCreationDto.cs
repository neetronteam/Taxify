using Taxify.Domain.Enums;

namespace Taxify.Service.DTOs.Drive;

public class DriveCreationDto
{
    public string Description { get; set; }
    public Decimal Price { get; set; }
    public PaymentType PaymentType { get; set; }
    public string Departure { get; set; }
    public string Destination { get; set; }
    public string Current { get; set; }
}
