using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Interfaces;
using Taxify.Service.Services;
using Taxify.Web.Models;

namespace Taxify.Web.Controllers
{
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
            var user = await userService.LoginAsync(dto);
            return View();
        }

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
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
}