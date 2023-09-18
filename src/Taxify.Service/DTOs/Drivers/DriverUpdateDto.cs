namespace Taxify.Service.DTOs.Drivers;

public class DriverUpdateDto
{
    public long Id { get; set; }
    public string CardNumber { get; set; }

    public long UserId { get; set; }
    public long AttachmentId { get; set; }
}
