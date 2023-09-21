using AutoMapper;
using Taxify.Domain.Entities;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;
using Taxify.Service.DTOs.Colors;
using Taxify.Domain.Configuration;
using Taxify.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Taxify.Service.Services;

public class ColorService : IColorService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public ColorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<ColorResultDto> AddAsync(ColorCreationDto dto)
    {
        var existColor = await this.unitOfWork
                        .ColorRepository
                        .SelectAsync(expression: color => color.Name.Equals(dto.Name));
        if (existColor is not null)
            throw new AlreadyExistsException(message: "This driver already exists");

        var mappedColor = this.mapper.Map<Color>(source: dto);
        await this.unitOfWork.ColorRepository.CreateAsync(entity: mappedColor);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<ColorResultDto>(source: mappedColor);
    }

    public async ValueTask<ColorResultDto> ModifyAsync(ColorUpdateDto dto)
    {
        var existColor = await this.unitOfWork
                        .ColorRepository
                        .SelectAsync(expression: color => color.Id.Equals(dto.Id))
            ?? throw new NotFoundException(message: "This color is not found");

        var mappedColor = this.mapper.Map(source: dto, destination: existColor);
        this.unitOfWork.ColorRepository.Update(entity: mappedColor);
        await this.unitOfWork.SaveAsync();

        return this.mapper.Map<ColorResultDto>(source: mappedColor);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var existColor = await this.unitOfWork
                        .ColorRepository
                        .SelectAsync(expression: color => color.Id.Equals(id))
            ?? throw new NotFoundException(message: "This color is not found");

        this.unitOfWork.ColorRepository.Delete(entity: existColor);
        await this.unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<bool> DestroyAsync(long id)
    {
        var existColor = await this.unitOfWork
                        .ColorRepository
                        .SelectAsync(expression: color => color.Id.Equals(id))
            ?? throw new NotFoundException(message: "This color is not found");

        this.unitOfWork.ColorRepository.Destroy(entity: existColor);
        await this.unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<ColorResultDto> RetrieveByIdAsync(long id)
    {
        var existColor = await this.unitOfWork
                        .ColorRepository
                        .SelectAsync(expression: color => color.Id.Equals(id))
            ?? throw new NotFoundException(message: "This color is not found");

        return this.mapper.Map<ColorResultDto>(source: existColor);
    }

    public async ValueTask<IEnumerable<ColorResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var colors = await this.unitOfWork.ColorRepository
                     .SelectAll()
                     .ToPaginate(@params)
                     .ToListAsync();

        return this.mapper.Map<IEnumerable<ColorResultDto>>(source: colors);
    }
}