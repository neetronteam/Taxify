using Microsoft.AspNetCore.Mvc;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Vehicles;
using Taxify.Service.Interfaces;
using Taxify.WebApi.Models;

namespace Taxify.WebApi.Controllers;

public class VehiclesController : BaseController
{
    private readonly IVehicleService vehicleService; 
    public VehiclesController(IVehicleService vehicleService)
    {
        this.vehicleService = vehicleService;
    }

    [HttpPost("Create")]
    public async ValueTask<IActionResult> Post(VehicleCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.vehicleService.AddAsync(dto)
        });
    }

    [HttpPut("Update")]
    public async ValueTask<IActionResult> Put(VehicleUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.vehicleService.ModifyAsync(dto)
        });
    }

    [HttpGet("Get")]
    public async ValueTask<IActionResult> Get(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.vehicleService.RetrieveByIdAsync(id)
        });
    }

    [HttpPut("GetAll")]
    public async ValueTask<IActionResult> GetAll([FromQuery]PaginationParams @params)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.vehicleService.RetrieveAllAsync(@params)
        });
    }
}
