using AutoMapper;
using Taxify.DataAccess.Contracts;
using Taxify.DataAccess.Repositories;
using Taxify.Service.Interfaces;
using Taxify.Service.Mapper;
using Taxify.Service.Services;

namespace Taxify.Web.Exstentions;

public static class CollectionService
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //services.AddAutoMapper(typeof(MappingProfile));
        //services.AddScoped<ICarService, CarService>();
        //services.AddScoped<IColorService, ColorService>();
        //services.AddScoped<IAttachmentService, AttachmentService>();
        //ervices.AddScoped<IUserService, UserService>();
    }
}
