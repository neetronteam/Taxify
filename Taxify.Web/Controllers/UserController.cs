using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Taxify.Service.DTOs.Attachments;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Interfaces;
using Taxify.Web.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Taxify.Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService userService;
    private readonly IAttachmentService attachmentService;
    private readonly ILogger<HomeController> _logger;
    public UserController(IUserService userService, ILogger<HomeController> logger, IAttachmentService attachmentService)
    {
        _logger = logger;
        this.userService = userService;
        this.attachmentService = attachmentService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ImageUpload(UserModel model)
    {
        ClaimsPrincipal claimsUser =  HttpContext.User;
        string Phone = claimsUser.FindFirst(ClaimTypes.MobilePhone).Value;      
        
        var user = await this.userService.RetrieveByPhoneAsync(Phone);

        var attachmentCreation = new AttachmentCreationDto{
            FormFile = model.file  
        };

        var result = await this.userService.UploadImageAsync(user.Id,attachmentCreation);

        var userModel = new UserModel
        {
            FirstName = result.Firstname,
            LastName = result.Lastname,
            Phone = result.Phone,
            Image = result.Attachment.FilePath,
            Username = result.Username
        };

        return RedirectToAction("Profile", "User", userModel);
    }

    public async Task<IActionResult> Profile()
    {
        ClaimsPrincipal claimsUser =  HttpContext.User;
        string Username = claimsUser.FindFirst(ClaimTypes.GivenName).Value;  
        string Firstname = claimsUser.FindFirst(ClaimTypes.Name).Value;  
        string Lastname = claimsUser.FindFirst(ClaimTypes.Surname).Value;  
        string Phone = claimsUser.FindFirst(ClaimTypes.MobilePhone).Value;  

        var result = await this.userService.RetrieveByPhoneAsync(Phone);
        
        var userModel = new UserModel
        {
            Username = Username,
            FirstName = Firstname,
            LastName = Lastname,
            Phone = Phone,
            Image = result.Attachment.FilePath.Replace("D:\\Projects\\Taxify\\Taxify.Web\\wwwroot\\","")
        };

        return View(userModel);
    }
}
