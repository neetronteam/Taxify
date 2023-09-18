using Taxify.Domain.Entities;

namespace Taxify.Service.DTOs.Messages;

public class MessageCreationDto
{
    public string Content { get; set; }
    public long SenderId { get; set; }
    public long ReceiveId { get; set; }
    public long? AttachmentId { get; set; }
}
