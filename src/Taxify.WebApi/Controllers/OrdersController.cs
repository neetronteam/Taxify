using Microsoft.AspNetCore.Mvc;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Orders;
using Taxify.Service.Interfaces;
using Taxify.WebApi.Models;

namespace Taxify.WebApi.Controllers;

public class OrdersController:BaseController
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async ValueTask<IActionResult> CreateAsync(OrderCreationDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.AddAsync(dto)
        });

    [HttpPut("update")]
    public async ValueTask<IActionResult> UpdateAsync(OrderUpdateDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.ModifyAsync(dto)
        });

    [HttpDelete("delete")]
    public async ValueTask<IActionResult> DeleteAsync(long orderId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RemoveAsync(orderId)
        });
    
    [HttpDelete("destroy")]
    public async ValueTask<IActionResult> DestroyAsync(long orderId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.DestroyAsync(orderId)
        });

    [HttpGet("get-by-id")]
    public async ValueTask<IActionResult> GetByIdAsync(long orderId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RetrieveByIdAsync(orderId)
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