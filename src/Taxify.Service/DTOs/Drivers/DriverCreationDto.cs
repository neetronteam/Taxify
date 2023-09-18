using Taxify.Domain.Entities;

namespace Taxify.Service.DTOs.Drivers;

public class DriverCreationDto
{
    public string CardNumber { get; set; }
    public long UserId { get; set; }
    public long AttachmentId { get; set; }
}
