using Microsoft.AspNetCore.Mvc;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.CarModels;
using Taxify.Service.Interfaces;
using Taxify.WebApi.Models;

namespace Taxify.WebApi.Controllers;

public class CarModelsController:BaseController
{
    private readonly ICarModelService _service;

    public CarModelsController(ICarModelService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async ValueTask<IActionResult> CreateAsync(CarModelCreationDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _service.AddAsync(dto)
        });

    [HttpPut("update")]
    public async ValueTask<IActionResult> UpdateAsync(CarModelUpdateDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _service.ModifyAsync(dto)
        });

    [HttpDelete("delete")]
    public async ValueTask<IActionResult> DeleteAsync(long carModelId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _service.RemoveAsync(carModelId)
        });
    
    [HttpDelete("destroy")]
    public async ValueTask<IActionResult> DestroyAsync(long carModelId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _service.DestroyAsync(carModelId)
        });
    
    [HttpGet("get-by-id")]
    public async ValueTask<IActionResult> GetByIdAsync(long carModelId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _service.RetrieveByIdAsync(carModelId)
        });
    
    
    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _service.RetrieveAllAsync(@params)
        });
}