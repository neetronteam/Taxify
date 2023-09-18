using Microsoft.AspNetCore.Http;

namespace Taxify.Service.DTOs.Attachments;

public class AttachmentUpdateDto
{
    public long Id { get; set; }
    public IFormFile FormFile { get; set; }
}
