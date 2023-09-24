using Taxify.WebApi.Models;
using Taxify.WebApi.Controller;
using Microsoft.AspNetCore.Mvc;
using Taxify.Service.Interfaces;
using Taxify.Service.DTOs.Colors;
using Taxify.Domain.Configuration;

namespace Taxify.WebApi.Controllers;

public class ColorsController : BaseController
{
    private readonly IColorService colorService;
    public ColorsController(IColorService colorService)
    {
        this.colorService = colorService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(ColorCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.colorService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(ColorUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.colorService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.colorService.RemoveAsync(id)
        });

    [HttpDelete("destroy/{id:long}")]
    public async Task<IActionResult> DestroyAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.colorService.DestroyAsync(id)
        });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.colorService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pagination)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.colorService.RetrieveAllAsync(pagination)
        });
}