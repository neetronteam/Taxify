using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Taxify.DataAccess.Contexts;
using Taxify.DataAccess.Contracts;
using Taxify.DataAccess.Repositories;
using Taxify.Service.Interfaces;
using Taxify.Service.Mapper;
using Taxify.Service.Services;

namespace Taxify.WebApi.Extensions;

public static class CollectionServices
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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

        services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        services.AddScoped<IUserService,UserService>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IAttachmentService,AttachmentService>();
    }
}