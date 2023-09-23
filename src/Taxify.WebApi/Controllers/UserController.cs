using Microsoft.AspNetCore.Mvc;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Interfaces;
using Taxify.WebApi.Models;

namespace Taxify.WebApi.Controller;

    [ApiController]
    [Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(UserCreationDto dto)
    {
        return Ok(new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.AddAsync(dto) 
        });
    }
}
//$2a$11$akTSqMfoa6V4b1CYQjuOY.F2xCuMPMvlbhN9o8Rg65r4erpXI4DO2