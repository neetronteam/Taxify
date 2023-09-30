using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Services;
using Taxify.Web.Models;
using Taxify.Service.Interfaces;

namespace Taxify.Web.Controllers;

public class AuthController : Controller
{
    private readonly IUserService userService;
    private readonly ILogger<HomeController> _logger;

    public AuthController(ILogger<HomeController> logger, IUserService userService)
    {
        _logger = logger;
        this.userService = userService;
    }   
    public IActionResult Index(LoginModel model)
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Main", "Home");

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
        {
        var userLoginDto = new UserLoginDto
        {
            Password = model.Password,
            Phone = model.Phone
        };

        try
        {
            var user = await userService.LoginAsync(userLoginDto);
            if (user is not null)
            { 
                List<Claim> claims = new List<Claim>() { 
                    new Claim(ClaimTypes.OtherPhone, model.Phone),
                    new Claim("OtherProperties","Example Role")
            
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme );

                AuthenticationProperties properties = new AuthenticationProperties() { 
            
                    AllowRefresh = true,
                    IsPersistent = model.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
        
                return RedirectToAction("Main", "Home");

            }
        }
        catch(Exception ex)
        {
            TempData["Message"] = ex.Message;
        }
        return RedirectToAction(actionName:"Index", routeValues: model);
    }

}
