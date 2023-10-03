using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Taxify.Domain.Entities;
using Taxify.Domain.Enums;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        { 
            Id = 1, 
            Firstname = "Test", 
            Lastname = "Testov", 
            Phone = "+998881234567", 
            Username = "Testbek", 
            Password = BCrypt.Net.BCrypt.HashPassword(inputKey: "12345") ,
            Role = Role.Admin, 
            IsDeleted = false, 
            CreatedAt = DateTime.UtcNow, 
            UpdatedAt = null 
        });
    }
}