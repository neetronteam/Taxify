using Taxify.Service.DTOs.Attachments;
using Taxify.Service.DTOs.Drivers;
using Taxify.Service.DTOs.Users;

namespace Taxify.Service.DTOs.Messages;

public class MessageResultDto
{
    public long Id { get; set; }
    public string Content { get; set; }
    public UserResultDto SenderId { get; set; }
    public DriverResultDto ReceiveId { get; set; }
    public AttachmentResultDto? Attachment { get; set; }
}