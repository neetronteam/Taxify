namespace Taxify.Service.DTOs.Cars;

public class CarUpdateDto
{
    public long Id { get; set; }
    public string Number { get; set; }
    public long CarModelId { get; set; }
    public long ColorId { get; set; }
    public long AttachmentId { get; set; }
}
