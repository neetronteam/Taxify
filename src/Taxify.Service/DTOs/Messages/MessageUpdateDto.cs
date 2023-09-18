namespace Taxify.Service.DTOs.Messages;

public class MessageUpdateDto
{
    public long Id { get; set; }
    public string Content { get; set; }
    public long SenderId { get; set; }
    public long ReceiveId { get; set; }
    public long? AttachmentId { get; set; }
}
