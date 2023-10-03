using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Interfaces;
using Taxify.Web.Models;

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
        {
            return RedirectToAction("Main", "Home");
        }
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
            if (user.Role == Domain.Enums.Role.Admin)
            {
                List<Claim> claims = new List<Claim>();
                if (user.Attachment is not null)
                {
                    claims.Add(new Claim(ClaimTypes.CookiePath, user.Attachment.FileName));
                }
                claims.Add(new Claim(ClaimTypes.MobilePhone, model.Phone));
                claims.Add(new Claim(ClaimTypes.Name, user.Firstname));
                claims.Add(new Claim(ClaimTypes.Surname, user.Lastname));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                claims.Add(new Claim(ClaimTypes.GivenName, user.Username));
                claims.Add(new Claim(ClaimTypes.PrimarySid, $"{user.Id}"));
                claims.Add(new Claim("OtherProperties", "Example Role"));

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = model.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Main", "Home");
            }
            else
            {
                TempData["Message"] = "This user is not admin";
            }
        }
        catch (Exception ex)
        {
            TempData["Message"] = ex.Message;
        }
        return RedirectToAction(actionName: "Index", routeValues: model);
    }

}
