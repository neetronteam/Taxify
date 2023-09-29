using Microsoft.AspNetCore.Mvc;

namespace Taxify.Web.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


}
