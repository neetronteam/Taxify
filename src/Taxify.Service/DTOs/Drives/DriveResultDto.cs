using Taxify.Domain.Enums;

namespace Taxify.Service.DTOs.Drive;

public class DriveResultDto
{
    public long Id { get; set; }
    public string Description { get; set; }
    public Decimal Price { get; set; }
    public PaymentType PaymentType { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Current { get; set; }
}
