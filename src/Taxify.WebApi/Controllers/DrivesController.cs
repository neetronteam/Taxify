using Microsoft.AspNetCore.Mvc;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Drive;
using Taxify.Service.Interfaces;
using Taxify.WebApi.Models;

namespace Taxify.WebApi.Controllers;

public class DrivesController:BaseController
{
    private readonly IDriveService _service;

    public DrivesController(IDriveService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async ValueTask<IActionResult> CreateAsync(DriveCreationDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.AddAsync(dto)
        });
    
    [HttpPut("update")]
    public async ValueTask<IActionResult> UpdateAsync(DriveUpdateDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.ModifyAsync(dto)
        });
    
    [HttpDelete("delete")]
    public async ValueTask<IActionResult> DeleteAsync(long driveId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RemoveAsync(driveId)
        });
    
    [HttpDelete("destroy")]
    public async ValueTask<IActionResult> DestroyAsync(long driveId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.DestroyAsync(driveId)
        });
    
    [HttpGet("get-by-id")]
    public async ValueTask<IActionResult> GetByIdAsync(long driveId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RetrieveByIdAsync(driveId)
        });
    
    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RetrieveAllAsync(@params)
        });

    //[HttpGet("filter-by-departure")]
    //public async ValueTask<IActionResult> FilterByDepartureAsync(string departure, [FromQuery] PaginationParams @params)
    //    => Ok(new Response()
    //    {
    //        StatusCode = 200,
    //        Message = "Success",
    //        Data = await _service.FilterByDepartureAsync(departure, @params)
    //    });
    
    
    //[HttpGet("filter-by-destination")]
    //public async ValueTask<IActionResult> FilterByDestinationAsync(string destination, [FromQuery] PaginationParams @params)
    //    => Ok(new Response()
    //    {
    //        StatusCode = 200,
    //        Message = "Success",
    //        Data = await _service.FilterByDestinationAsync(destination, @params)
    //    });
    
    
    //[HttpGet("filter-by-departure")]
    //public async ValueTask<IActionResult> FilterByDepartureAndDestinationAsync(string departure,string destination, [FromQuery] PaginationParams @params)
    //    => Ok(new Response()
    //    {
    //        StatusCode = 200,
    //        Message = "Success",
    //        Data = await _service.FilterByDepartureAndDestination(departure, destination,@params)
    //    });
}