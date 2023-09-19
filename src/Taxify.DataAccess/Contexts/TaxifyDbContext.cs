using Microsoft.EntityFrameworkCore;
using Taxify.Domain.Entities;

namespace Taxify.DataAccess.Contexts;

public class TaxifyDbContext : DbContext
{
    public TaxifyDbContext(DbContextOptions<TaxifyDbContext> options) : base(options)
    { }

    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Drive> Drives { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
}