using Taxify.Domain.Entities;
using Taxify.DataAccess.Contexts;
using Taxify.DataAccess.Contracts;

namespace Taxify.DataAccess.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly ITaxifyDbContext _context;

    public UnitOfWork(ITaxifyDbContext context)
    {
        _context = context;
        CarRepository = new Repository<Car>(_context);
        CarModelRepository = new Repository<CarModel>(_context);
        ColorRepository = new Repository<Color>(_context);
        DriveRepository = new Repository<Drive>(_context);
        DriverRepository = new Repository<Driver>(_context);
        MessageRepository = new Repository<Message>(_context);
        OrderRepository = new Repository<Order>(_context);
        UserRepository = new Repository<User>(_context);
        VehicleRepository = new Repository<Vehicle>(_context);
        AttachmentRepository = new Repository<Attachment>(_context);
    }

    public IRepository<Car> CarRepository { get; }
    public IRepository<CarModel> CarModelRepository { get; }
    public IRepository<Color> ColorRepository { get; }
    public IRepository<Drive> DriveRepository { get; }
    public IRepository<Driver> DriverRepository { get; }
    public IRepository<Message> MessageRepository { get; }
    public IRepository<Order> OrderRepository { get; }
    public IRepository<User> UserRepository { get; }
    public IRepository<Vehicle> VehicleRepository { get; }
    public IRepository<Attachment> AttachmentRepository { get; }

    public async ValueTask SaveAsync() => await _context.SaveChangesAsync();
    
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}