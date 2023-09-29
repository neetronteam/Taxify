using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Interfaces;
using Taxify.Web.Models;

namespace Taxify.Web.Controllers;
[System.Web.Mvc.HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
public class HomeController : Controller
{
    private readonly IUserService userService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IUserService userService)
    {
        _logger = logger;
        this.userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Main(UserLoginDto dto)
    {
        try
        {
        var user = await userService.LoginAsync(dto);
            return RedirectToAction(actionName: "Main");
        }
        catch(Exception ex)
        {
            TempData["Message"] = ex.Message;
            return RedirectToAction(actionName:"Index",routeValues: dto);
        }
    }

    public IActionResult Main()
    {
        return View();
    }

    public IActionResult Index(UserLoginDto dto)
    {
        return View(dto);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}