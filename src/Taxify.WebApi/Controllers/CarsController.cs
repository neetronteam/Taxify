using Taxify.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Taxify.Service.DTOs.Cars;
using Taxify.WebApi.Controllers;
using Taxify.Service.Interfaces;
using Taxify.Domain.Configuration;

namespace Taxify.WebApi.Controllers;

public class CarsController : BaseController
{
    private readonly ICarService carService;
    public CarsController(ICarService carService)
    {
        this.carService = carService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(CarCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.carService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(CarUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.carService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.carService.RemoveAsync(id)
        });

    [HttpDelete("destroy/{id:long}")]
    public async Task<IActionResult> DestroyAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.carService.DestroyAsync(id)
        });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.carService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pagination)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.carService.RetrieveAllAsync(pagination)
        });
}