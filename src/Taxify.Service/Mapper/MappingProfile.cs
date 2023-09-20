using AutoMapper;
using Taxify.Domain.Entities;
using Taxify.Service.DTOs.Cars;
using Taxify.Service.DTOs.Users;
using Taxify.Service.DTOs.Drive;
using Taxify.Service.DTOs.Orders;
using Taxify.Service.DTOs.Colors;
using Taxify.Service.DTOs.Drivers;
using Taxify.Service.DTOs.Vehicles;
using Taxify.Service.DTOs.Messages;
using Taxify.Service.DTOs.CarModels;
using Taxify.Service.DTOs.Attachments;

namespace Taxify.Service.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User
        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();

        //Driver
        CreateMap<Driver, DriverCreationDto>().ReverseMap();
        CreateMap<Driver, DriverUpdateDto>().ReverseMap();
        CreateMap<Drive, DriverResultDto>().ReverseMap();

        //Drive
        CreateMap<Drive, DriveCreationDto>().ReverseMap();
        CreateMap<Drive, DriveUpdateDto>().ReverseMap();
        CreateMap<Drive, DriveResultDto>().ReverseMap();

        //Vehicle
        CreateMap<Vehicle, VehicleCreationDto>().ReverseMap();
        CreateMap<Vehicle, VehicleResultDto>().ReverseMap();
        CreateMap<Vehicle, VehicleUpdateDto>().ReverseMap();

        //Car
        CreateMap<Car, CarCreationDto>().ReverseMap();
        CreateMap<Car, CarResultDto>().ReverseMap();
        CreateMap<Car, CarUpdateDto>().ReverseMap();

        //CarModel
        CreateMap<CarModel, CarModelCreationDto>().ReverseMap();
        CreateMap<CarModel, CarModelResultDto>().ReverseMap();
        CreateMap<CarModel, CarModelUpdateDto>().ReverseMap();

        //Color
        CreateMap<Color, ColorCreationDto>().ReverseMap();
        CreateMap<Color, ColorUpdateDto>().ReverseMap();
        CreateMap<Color, ColorResultDto>().ReverseMap();

        //Order 
        CreateMap<Order, OrderCreationDto>().ReverseMap();
        CreateMap<Order, OrderResultDto>().ReverseMap();
        CreateMap<Order, OrderUpdateDto>().ReverseMap();

        //Attachment
        CreateMap<Attachment, AttachmentCreationDto>().ReverseMap();
        CreateMap<Attachment, AttachmentUpdateDto>().ReverseMap();
        CreateMap<Attachment, AttachmentResultDto>().ReverseMap();

        //Message
        CreateMap<Message, MessageCreationDto>().ReverseMap();
        CreateMap<Message, MessageUpdateDto>().ReverseMap();
        CreateMap<Message, MessageResultDto>().ReverseMap();
    }
}
