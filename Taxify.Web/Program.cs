using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Principal;
using Taxify.DataAccess.Contexts;
using Taxify.DataAccess.Contracts;
using Taxify.DataAccess.Repositories;
using Taxify.Service.Interfaces;
using Taxify.Service.Mapper;
using Taxify.Service.Services;
using Taxify.Web.Exstentions;

var builder = WebApplication.CreateBuilder(args);
builder. Services.AddRazorPages();

//Authentication
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Auth/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    
    });

// Add services to the container.
builder.Services.AddDbContext<ITaxifyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAttachmentService,AttachmentService>();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
/*
builder.Services.AddDbContext<TaxifyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
*/

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapRazorPages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

    app.Run();


