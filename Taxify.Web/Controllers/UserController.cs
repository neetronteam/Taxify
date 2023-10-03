using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Attachments;
using Taxify.Service.DTOs.Users;
using Taxify.Service.Interfaces;
using Taxify.Web.Models;

namespace Taxify.Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService userService;
    private readonly IAttachmentService attachmentService;
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper mapper;
    public UserController(IUserService userService, ILogger<HomeController> logger, IAttachmentService attachmentService, IMapper mapper)
    {
        _logger = logger;
        this.userService = userService;
        this.attachmentService = attachmentService;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Edit(User user)
    {
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Editer(User user)
    {
        var mappedUser = mapper.Map<UserUpdateDto>(user);
        await this.userService.ModifyAsync(mappedUser);

        return RedirectToAction("Edit", user);
    }

    public async ValueTask<IActionResult> Users()
    {
        var result = await this.userService.RetrieveAllAsync();
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UserModel model)
    {
        ClaimsPrincipal claimsUser = HttpContext.User;
        string userId = claimsUser.FindFirst(ClaimTypes.PrimarySid).Value;

        var user = await this.userService.RetrieveByIdAsync(long.Parse(userId));

        var userUpdateDto = new UserUpdateDto
        {
            Id = long.Parse(userId),
            Firstname = model.FirstName,
            Lastname = model.LastName,
            Phone = model.Phone,
            Username = model.Username,
            Role = user.Role,
            Gender = user.Gender,
        };

        var result = await this.userService.ModifyAsync(userUpdateDto);

        return RedirectToAction("Profile", "User", routeValues: model);
    }

    [HttpPost]
    public async Task<IActionResult> ImageUpload(UserModel model)
    {
        if (model.file is null)
        {
            TempData["Message"] = "Please upload image!";
            return RedirectToAction("Profile", "User");
        }

        ClaimsPrincipal claimsUser = HttpContext.User;
        string userId = claimsUser.FindFirst(ClaimTypes.PrimarySid).Value;

        var user = await this.userService.RetrieveByIdAsync(long.Parse(userId));

        var attachmentCreation = new AttachmentCreationDto
        {
            FormFile = model.file
        };

        var result = await this.userService.UploadImageAsync(user.Id, attachmentCreation);

        var userModel = new UserModel
        {
            FirstName = result.Firstname,
            LastName = result.Lastname,
            Phone = result.Phone,
            ImageName = result.Attachment.FilePath,
            Username = result.Username,
            ImagePath = result.Attachment.FileName
        };

        return RedirectToAction("Profile", "User", userModel);
    }

    public async Task<IActionResult> Profile()
    {
        ClaimsPrincipal claimsUser = HttpContext.User;

        long userId = long.Parse(claimsUser.FindFirst(ClaimTypes.PrimarySid).Value);

        var result = await this.userService.RetrieveByIdAsync(userId);

        var userModel = new UserModel();

        if (result.Attachment is not null)
        {
            userModel = new UserModel
            {
                Username = result.Username,
                FirstName = result.Firstname,
                LastName = result.Lastname,
                Phone = result.Phone,
                ImagePath = result.Attachment.FilePath,
                ImageName = result.Attachment.FileName
            };
        }
        else
        {
            userModel = new UserModel
            {
                Username = result.Username,
                FirstName = result.Firstname,
                LastName = result.Lastname,
                Phone = result.Phone,
                ImageName = null
            };
        }
        return View(userModel);
    }

}
