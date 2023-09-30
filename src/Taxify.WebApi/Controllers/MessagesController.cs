using Microsoft.AspNetCore.Mvc;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Messages;
using Taxify.Service.Interfaces;
using Taxify.WebApi.Models;

namespace Taxify.WebApi.Controllers;

public class MessagesController:BaseController
{
    private readonly IMessageService _service;

    public MessagesController(IMessageService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async ValueTask<IActionResult> CreateAsync(MessageCreationDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.AddAsync(dto)
        });
    
    [HttpPut("update")]
    public async ValueTask<IActionResult> UpdateAsync(MessageUpdateDto dto)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.ModifyAsync(dto)
        });
    
    [HttpDelete("delete")]
    public async ValueTask<IActionResult> DeleteAsync(long messageId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RemoveAsync(messageId)
        });
    
    [HttpDelete("destroy")]
    public async ValueTask<IActionResult> DestroyAsync(long messageId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.DestroyAsync(messageId)
        });

    [HttpGet("get-by-id")]
    public async ValueTask<IActionResult> GetByIdAsync(long messageId)
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RetrieveByIdAsync(messageId)
        });
    
    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync()
        => Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _service.RetrieveAllAsync()
        });
}