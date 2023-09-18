using Taxify.Domain.Entities;

namespace Taxify.Service.DTOs.Cars;

public class CarCreationDto
{
    public string Number { get; set; }
    public long CarModelId { get; set; }
    public long ColorId { get; set; }
    public long AttachmentId { get; set; }
}
