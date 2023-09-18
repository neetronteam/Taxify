using Taxify.Domain.Entities;

namespace Taxify.Service.DTOs.Messages;

public class MessageCreationDto
{
    public string Content { get; set; }
    public long FromId { get; set; }
    public long ToId { get; set; }
    public long AttachmentId { get; set; }
}
