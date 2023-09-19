using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taxify.DataAccess.Contracts;
using Taxify.Domain.Configuration;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Drive;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;

namespace Taxify.Service.Services;

public class DriveService:IDriveService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DriveService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<DriveResultDto> AddAsync(DriveCreationDto dto)
    {
        var drive = _mapper.Map<Drive>(source: dto);

        await _unitOfWork.DriveRepository.CreateAsync(entity: drive);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<DriveResultDto>(source: drive);
    }

    public async ValueTask<DriveResultDto> ModifyAsync(DriveUpdateDto dto)
    {
        var existDrive = await _unitOfWork.DriveRepository
                             .SelectAsync(expression: drive => drive.IsDeleted == false && drive.Id == dto.Id)
                         ?? throw new NotFoundException(message: "Drive is not found");

        var mappedDrive = _mapper.Map<Drive>(source: existDrive);

        _unitOfWork.DriveRepository.Update(entity: mappedDrive);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<DriveResultDto>(source: mappedDrive);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var existDrive = await _unitOfWork.DriveRepository
                             .SelectAsync(expression: drive => drive.IsDeleted == false && drive.Id == id)
                         ?? throw new NotFoundException(message: "Drive is not found");
        
        _unitOfWork.DriveRepository.Delete(entity:existDrive);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<DriveResultDto> RetrieveAsync(long id)
    {
        var existDrive = await _unitOfWork.DriveRepository
                             .SelectAsync(expression: drive => drive.IsDeleted == false && drive.Id == id)
                         ?? throw new NotFoundException(message: "Drive is not found");

        return _mapper.Map<DriveResultDto>(source: existDrive);
    }

    public async ValueTask<IEnumerable<DriveResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var drives = await _unitOfWork.DriveRepository
                                .SelectAll(expression: drive => drive.IsDeleted == false)
                                .ToPaginate(@params)
                                .ToListAsync();

        return _mapper.Map<IEnumerable<DriveResultDto>>(source: drives);
    }
}