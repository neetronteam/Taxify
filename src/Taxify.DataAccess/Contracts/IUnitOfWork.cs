using Taxify.Domain.Entities;

namespace Taxify.DataAccess.Contracts;

public interface IUnitOfWork:IDisposable
{
    IRepository<Car> CarRepository { get; }
    IRepository<CarModel> CarModelRepository { get; }
    IRepository<Color> ColorRepository { get; }
    IRepository<Drive> DriveRepository { get; }
    IRepository<Driver> DriverRepository { get; }
    IRepository<Message> MessageRepository { get; }
    IRepository<Order> OrderRepository { get; }
    IRepository<User> UserRepository { get; }
    IRepository<Vehicle> VehicleRepository { get; }
    IRepository<Attachment> AttachmentRepository { get; }

    ValueTask SaveAsync();
}