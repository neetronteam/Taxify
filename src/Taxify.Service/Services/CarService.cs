using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taxify.DataAccess.Contracts;
using Taxify.Domain.Configuration;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Cars;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;

namespace Taxify.Service.Services;

public class CarService : ICarService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CarService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async ValueTask<CarResultDto> AddAsync(CarCreationDto dto)
    {
        var exsistCar = await _unitOfWork.CarRepository
            .SelectAsync(c => c.Number.Equals(dto.Number));
        if (exsistCar is not null)
            throw new AlreadyExistsException("Car is already exsist with this number");

        var mappedCar = _mapper.Map<Car>(dto);
        
        await _unitOfWork.CarRepository.CreateAsync(mappedCar);
        await _unitOfWork.SaveAsync();
        
        return _mapper.Map<CarResultDto>(mappedCar);
    }

    public async ValueTask<CarResultDto> ModifyAsync(CarUpdateDto dto)
    {
        var exsistCar = await _unitOfWork.CarRepository
            .SelectAsync(c => c.Id.Equals(dto.Id));
        
        if (exsistCar is null)
            throw new NotFoundException("Car not found with this id");

        if (exsistCar.Number != dto.Number)
        {
            var exsistCar2 = await _unitOfWork.CarRepository
                .SelectAsync(c => c.Number.Equals(dto.Number));
            
            if(exsistCar2 is not null)
                throw new AlreadyExistsException("Car is already exsist with this number");
        }

        _mapper.Map(dto, exsistCar);
        
        _unitOfWork.CarRepository.Update(exsistCar);
        await _unitOfWork.SaveAsync();
        
        return _mapper.Map<CarResultDto>(exsistCar);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var exsistCar = await _unitOfWork.CarRepository
            .SelectAsync(c => c.Id.Equals(id));
        
        if (exsistCar is null)
            throw new NotFoundException("Car not found with this id");
        
        _unitOfWork.CarRepository.Delete(exsistCar);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<bool> DestroyAsync(long id)
    {
        var exsistCar = await _unitOfWork.CarRepository
            .SelectAsync(c => c.Id.Equals(id));
        
        if (exsistCar is null)
            throw new NotFoundException("Car not found with this id");
        
        _unitOfWork.CarRepository.Destroy(exsistCar);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<CarResultDto> RetrieveByIdAsync(long id)
    {
        var exsistCar = await _unitOfWork.CarRepository
            .SelectAsync(c => c.Id.Equals(id));
        
        if (exsistCar is null)
            throw new NotFoundException("Car not found with this id");

        return _mapper.Map<CarResultDto>(exsistCar);
    }

    public async ValueTask<IEnumerable<CarResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var cars = await _unitOfWork.CarRepository
            .SelectAll()
            .ToPaginate(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CarResultDto>>(cars);
    }
}