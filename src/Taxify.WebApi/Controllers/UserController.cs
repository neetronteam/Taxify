using Microsoft.AspNetCore.Mvc;
using Taxify.Domain.Configuration;
using Taxify.Service.DTOs.Attachments;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Interfaces;
using Taxify.WebApi.Models;

namespace Taxify.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(UserCreationDto dto)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.RegisterAsync(dto) 
        });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(UserLoginDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.LoginAsync(dto)
        });
    }


    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAsync(UserUpdateDto dto)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.ModifyAsync(dto) 
        });
    }

    [HttpGet("Get/{Id}")]
    public async Task<IActionResult> GetByIdAsync(long Id)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.RetrieveByIdAsync(Id) 
        });
    }

    [HttpGet("GetAll")]
    public IActionResult GetAllAsync([FromQuery]PaginationParams @params)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data =  this.userService.RetrieveAllAsync(@params) 
        });
    }

    [HttpPost("UploadImage")]
    public async Task<IActionResult> UploadImageAsync(long userId, [FromForm] AttachmentCreationDto dto)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.UploadImageAsync(userId,dto) 
        });
    }

    [HttpPost("UpdatePassword")]
    public async Task<IActionResult> ChangePasswordAsync(long userId, string oldPassword, string newPassword)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.UpdatePasswordAsync(userId, oldPassword, newPassword) 
        });
    }
}
//$2a$11$akTSqMfoa6V4b1CYQjuOY.F2xCuMPMvlbhN9o8Rg65r4erpXI4DO2