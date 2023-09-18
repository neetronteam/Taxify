using Taxify.Service.DTOs.Attachments;
using Taxify.Service.DTOs.Users;

namespace Taxify.Service.DTOs.Drivers;

public class DriverResultDto
{
    public long Id { get; set; }
    public string CardNumber { get; set; }

    public UserResultDto User { get; set; }
    public AttachmentResultDto Attachment { get; set; }
}
