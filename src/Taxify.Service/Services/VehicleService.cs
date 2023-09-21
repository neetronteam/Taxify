using AutoMapper;
using Taxify.Domain.Entities;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;
using Taxify.Domain.Configuration;
using Taxify.DataAccess.Contracts;
using Taxify.Service.DTOs.Vehicles;

namespace Taxify.Service.Services;

public class VehicleService : IVehicleService
{
    private readonly IUnitOfWork unitOfWork;
    public readonly IMapper mapper;
    VehicleService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<VehicleResultDto> AddAsync(VehicleCreationDto dto)
    {
        var mappedVehicle = this.mapper.Map<Vehicle>(dto);

        await this.unitOfWork.VehicleRepository.CreateAsync(mappedVehicle);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<VehicleResultDto>(mappedVehicle);
    }

    public async ValueTask<bool> DestroyAsync(long id)
    {
        var vehicle = await this.unitOfWork.VehicleRepository
                                .SelectAsync(vehicle => vehicle.Id.Equals(id))
                                ?? throw new NotFoundException("Vehicle is not found");

        this.unitOfWork.VehicleRepository.Destroy(vehicle);
        await this.unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<VehicleResultDto> ModifyAsync(VehicleUpdateDto dto)
    {
        var existVehicle = await this.unitOfWork.VehicleRepository
                             .SelectAsync(vehicle => vehicle.IsDeleted == false && vehicle.Id == dto.Id)
                         ?? throw new NotFoundException(message: "Vehicle is not found");

        var mappedVehicle = this.mapper.Map<Vehicle>(source: existVehicle);

        this.unitOfWork.VehicleRepository.Update(mappedVehicle);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<VehicleResultDto>(source: mappedVehicle);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var vehicle = await this.unitOfWork.VehicleRepository
            .SelectAsync(vehicle => vehicle.Id.Equals(id) && vehicle.IsDeleted.Equals(false))
                ?? throw new NotFoundException("Vehicle is not found");

        this.unitOfWork.VehicleRepository.Delete(vehicle);
        await this.unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<VehicleResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var vehicles = this.unitOfWork.VehicleRepository.SelectAll(x => x.IsDeleted.Equals(false))
                                                        .ToPaginate(@params);
        return this.mapper.Map<IEnumerable<VehicleResultDto>>(vehicles);
    }

    public async ValueTask<VehicleResultDto> RetrieveAsync(long id)
    {
        var vehicle = await this.unitOfWork.VehicleRepository.
                                 SelectAsync(x => x.Id.Equals(id) && x.IsDeleted.Equals(false))
                                 ?? throw new NotFoundException("Vehicle not found");

        return this.mapper.Map<VehicleResultDto>(vehicle);
    }
}
