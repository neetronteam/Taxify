using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Attachments;

namespace Taxify.Service.Interfaces;

public interface IAttachmentService
{
    ValueTask<Attachment> UploadAsync(AttachmentCreationDto dto);
    ValueTask<bool> RemoveAsync(Attachment attachment);
}