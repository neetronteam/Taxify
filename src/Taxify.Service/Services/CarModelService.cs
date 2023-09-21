using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taxify.DataAccess.Contracts;
using Taxify.Domain.Configuration;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.CarModels;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;

namespace Taxify.Service.Services;

public class CarModelService : ICarModelService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CarModelService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async ValueTask<CarModelResultDto> AddAsync(CarModelCreationDto dto)
    {
        var exsistCarModel = await _unitOfWork.CarModelRepository
            .SelectAsync(c => c.Model.Equals(dto.Model));
        
        if (exsistCarModel is not null)
            throw new AlreadyExistsException("CarModel is already exsist");

        var mappedCarModel = _mapper.Map<CarModel>(dto);
        
        await _unitOfWork.CarModelRepository.CreateAsync(mappedCarModel);
        await _unitOfWork.SaveAsync();
        
        return _mapper.Map<CarModelResultDto>(mappedCarModel);
    }

    public async ValueTask<CarModelResultDto> ModifyAsync(CarModelUpdateDto dto)
    {
        var exsistCarModel = await _unitOfWork.CarModelRepository
            .SelectAsync(c => c.Id.Equals(dto.Id));
        
        if (exsistCarModel is null)
            throw new NotFoundException("CarModel not found with this id");

        _mapper.Map(dto, exsistCarModel);
        
        _unitOfWork.CarModelRepository.Update(exsistCarModel);
        await _unitOfWork.SaveAsync();
        
        return _mapper.Map<CarModelResultDto>(exsistCarModel);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var exsistCarModel = await _unitOfWork.CarModelRepository
            .SelectAsync(c => c.Id.Equals(id));
        
        if (exsistCarModel is null)
            throw new NotFoundException("Car Model not found with this id");
        
        _unitOfWork.CarModelRepository.Delete(exsistCarModel);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<bool> DestroyAsync(long id)
    {
        var exsistCarModel = await _unitOfWork.CarModelRepository
            .SelectAsync(c => c.Id.Equals(id));
        
        if (exsistCarModel is null)
            throw new NotFoundException("Car Model not found with this id");
        
        _unitOfWork.CarModelRepository.Destroy(exsistCarModel);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<CarModelResultDto> RetrieveByIdAsync(long id)
    {
        var exsistCarModel = await _unitOfWork.CarModelRepository
            .SelectAsync(c => c.Id.Equals(id));
        
        if (exsistCarModel is null)
            throw new NotFoundException("Car Model not found with this id");

        return _mapper.Map<CarModelResultDto>(exsistCarModel);
    }

    public async ValueTask<IEnumerable<CarModelResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var cars = await _unitOfWork.CarRepository
            .SelectAll()
            .ToPaginate(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CarModelResultDto>>(cars);
    }
}