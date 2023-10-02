using System.Text;
using Taxify.Service.Mapper;
using Taxify.Service.Services;
using Microsoft.OpenApi.Models;
using Taxify.Service.Interfaces;
using Taxify.DataAccess.Contracts;
using Microsoft.IdentityModel.Tokens;
using Taxify.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Taxify.WebApi.Extensions;

public static class CollectionServices
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IColorService, ColorService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDriveService, DriveService>();
        services.AddScoped<IDriverService, DriverService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IMessageService, MessageService>();  
        services.AddScoped<ICarModelService, CarModelService>();
        services.AddScoped<IAttachmentService, AttachmentService>();
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}