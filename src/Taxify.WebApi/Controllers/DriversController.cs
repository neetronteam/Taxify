using Microsoft.AspNetCore.Mvc;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Drivers;
using Taxify.Service.Interfaces;
using Taxify.WebApi.Models;

namespace Taxify.WebApi.Controllers;

public class DriversController:BaseController
{
    private readonly IDriverService _service;

    public DriversController(IDriverService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async ValueTask<IActionResult> CreateAsync(DriverCreationDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data =await _service.AddAsync(dto)
        });

    [HttpPut("update")]
    public async ValueTask<IActionResult> UpdateAsync(DriverUpdateDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.ModifyAsync(dto)
        });

    [HttpDelete("delete")]
    public async ValueTask<IActionResult> DeleteAsync(long driverId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RemoveAsync(driverId)
        });

    [HttpDelete("destroy")]
    public async ValueTask<IActionResult> DestroyAsync(long driverId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.DestroyAsync(driverId)
        });
    
    [HttpGet("get-by-id")]
    public async ValueTask<IActionResult> GetByIdAsync(long driverId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RetrieveByIdAsync(driverId)
        });

    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RetrieveAllAsync(@params)
        });

}