using AutoMapper;
using Taxify.Domain.Entities;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;
using Taxify.DataAccess.Contracts;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Drivers;
using Microsoft.EntityFrameworkCore;

namespace Taxify.Service.Services;

public class DriverService : IDriverService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public DriverService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<DriverResultDto> AddAsync(DriverCreationDto dto)
    {
        var existDriver = await this.unitOfWork.DriverRepository
                        .SelectAsync(expression: driver => driver.CardNumber.Equals(dto.CardNumber));
        if (existDriver is not null)
            throw new AlreadyExistsException(message: "This driver already exists");

        var user = await this.unitOfWork.UserRepository
            .SelectAsync(expression: user => user.Id.Equals(dto.UserId))
            ?? throw new NotFoundException(message: "This user is not found");

        var mappedDriver = this.mapper.Map<Driver>(source: dto);
        mappedDriver.User = user;

        await this.unitOfWork.DriverRepository.CreateAsync(entity: mappedDriver);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<DriverResultDto>(source: mappedDriver);
    }

    public async ValueTask<DriverResultDto> ModifyAsync(DriverUpdateDto dto)
    {
        var existDriver = await this.unitOfWork.DriverRepository
                        .SelectAsync(expression: driver => driver.Id.Equals(dto.Id))
                        ?? throw new NotFoundException(message: "This driver is not found");

        var user = await this.unitOfWork.UserRepository
                   .SelectAsync(expression: user => user.Id.Equals(dto.UserId))
                    ?? throw new NotFoundException(message: "This user is not found");

        var mappedDriver = this.mapper.Map(source: dto, destination: existDriver);
        mappedDriver.User = user;

        this.unitOfWork.DriverRepository.Update(entity: mappedDriver);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<DriverResultDto>(source: mappedDriver);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var existDriver = await this.unitOfWork.DriverRepository
                        .SelectAsync(expression: driver => driver.Id.Equals(id), includes: new[] { "User" })
                        ?? throw new NotFoundException(message: "This driver is not found");

        this.unitOfWork.DriverRepository.Delete(entity: existDriver);
        await this.unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<bool> DestroyAsync(long id)
    {
        var existDriver = await this.unitOfWork.DriverRepository
                        .SelectAsync(expression: driver => driver.Id.Equals(id), includes: new[] { "User" })
                        ?? throw new NotFoundException(message: "This driver is not found");

        this.unitOfWork.DriverRepository.Destroy(entity: existDriver);
        await this.unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<DriverResultDto> RetrieveByIdAsync(long id)
    {
        var existDriver = await this.unitOfWork.DriverRepository
                        .SelectAsync(expression: driver => driver.Id.Equals(id), includes: new[] { "User" })
                        ?? throw new NotFoundException(message: "This driver is not found");

        return this.mapper.Map<DriverResultDto>(source: existDriver);
    }

    public async ValueTask<IEnumerable<DriverResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var drivers = await this.unitOfWork.DriveRepository
                     .SelectAll()
                     .ToPaginate(@params)
                     .ToListAsync();

        return this.mapper.Map<IEnumerable<DriverResultDto>>(source: drivers);
    }
}