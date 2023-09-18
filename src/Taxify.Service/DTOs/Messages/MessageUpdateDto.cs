namespace Taxify.Service.DTOs.Messages;

public class MessageUpdateDto
{
    public long Id { get; set; }
    public string Content { get; set; }
    public long FromId { get; set; }
    public long ToId { get; set; }
    public long AttachmentId { get; set; }
}
