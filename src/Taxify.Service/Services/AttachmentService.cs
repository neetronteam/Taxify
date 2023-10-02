using AutoMapper;
using Taxify.DataAccess.Contracts;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Attachments;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Helpers;
using Taxify.Service.Interfaces;

namespace Taxify.Service.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async ValueTask<Attachment> UploadAsync(AttachmentCreationDto dto)
    {
        var webrootPath = Path.Combine(PathHelper.WebRootPath, "Images");

        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        var fileName = MediaHelper.MakeImageName(dto.FormFile.FileName);
        var fullPath = Path.Combine(webrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());

        var createdAttachment = new Attachment
        {
            FileName = fileName,
            FilePath = fullPath
        };

        await _unitOfWork.AttachmentRepository.CreateAsync(createdAttachment);
        await _unitOfWork.SaveAsync();

        return createdAttachment;
    }

    public async ValueTask<bool> RemoveAsync(Attachment attachment)
    {
        var existAttachment = await _unitOfWork.AttachmentRepository
            .SelectAsync(x => x.Id == attachment.Id);

        if (existAttachment is null)
            throw new NotFoundException("Attachment not found");

        _unitOfWork.AttachmentRepository.Delete(existAttachment);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<AttachmentResultDto> RetriveByIdAsync(long? id)
    {
        var result = await this._unitOfWork.AttachmentRepository.SelectAsync(x => x.Id == id);
        
        if(result is null)
        {
            return null;
        };
        
            var attachmentResultDto = new AttachmentResultDto()
            {
                FileName = result.FileName,
                FilePath = result.FilePath
            };
        return attachmentResultDto;
    }
}