using Microsoft.AspNetCore.Mvc;
using Taxify.Domain.Configuration;
using Taxify.Domain.Enums;
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
    public async Task<IActionResult> Register(UserCreationDto dto)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.RegisterAsync(dto) 
        });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.LoginAsync(dto)
        });
    }


    [HttpPost("Update")]
    public async Task<IActionResult> Update(UserUpdateDto dto)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.ModifyAsync(dto) 
        });
    }

    [HttpGet("Get/{Id}")]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.RetrieveByIdAsync(id) 
        });
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll([FromQuery]PaginationParams @params)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data =  this.userService.RetrieveAllAsync(@params) 
        });
    }

    [HttpPost("UploadImage")]
    public async Task<IActionResult> UploadImage(long userId, [FromForm] AttachmentCreationDto dto)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.UploadImageAsync(userId,dto) 
        });
    }

    [HttpPost("UpdatePassword")]
    public async Task<IActionResult> ChangePassword(long userId, string oldPassword, string newPassword)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.UpdatePasswordAsync(userId, oldPassword, newPassword) 
        });
    }

    [HttpPost("Destroy")]
    public async Task<IActionResult> Destroy(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.DestroyAsync(id) 
        });
    }

    [HttpPost("Remove")]
    public async Task<IActionResult> Remove(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.RemoveAsync(id) 
        });
    }

    [HttpPatch("upgrade-role")]
	public async ValueTask<IActionResult> UpgradeRoleAsync(long id, Role role)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.UpgradeRoleAsync(id, role)
		});
}
