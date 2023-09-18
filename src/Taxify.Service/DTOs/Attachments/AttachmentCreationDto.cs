using Microsoft.AspNetCore.Http;

namespace Taxify.Service.DTOs.Attachments;

public class AttachmentCreationDto
{
    public IFormFile FormFile { get; set; }
}
