using Microsoft.EntityFrameworkCore;
using Razor_Rentals.Data.Models;

namespace Razor_Rentals.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    Name = "Sebastian Fält",
                    Email = "sfa0110@hotmail.com",
                    Phone = "0761144194",
                    Password = "hejsan123",
                });
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    Name = "Christine Fält",
                    Email = "kicki@hotmail.com",
                    Phone = "0701144194",
                    Password = "hejsan123",
                });
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    CarId = 1,
                    Name = "Volvo XC 60",
                    Description = "4-Wheel driven, Gasoline (95), Year: 2020, 5 seats",
                    Available = true,
                    CostPerDay = 529,
                    ImageURL = "https://shorturl.at/bhiM2"

                });
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    CarId = 2,
                    Name = "Volvo 740",
                    Description = "Gammskräpe",
                    Available = true,
                    CostPerDay = 299,
                    ImageURL = "https://t.ly/HcTm6"

                });
        }
    }
}
