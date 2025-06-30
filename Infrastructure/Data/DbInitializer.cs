using Flota365.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Flota365.API.Domain.Enums;


namespace Flota365.API.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();
            
            // Check if data already exists
            if (await context.Users.AnyAsync())
                return; // Database has been seeded
            
            // Seed Users
            var users = new[]
            {
                new User
                {
                    FirstName = "Juan",
                    LastName = "Supervisor",
                    Email = "juan@flota365.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Administrator"
                }
            };
            
            await context.Users.AddRangeAsync(users);
            
            // Seed Fleets
            var fleets = new[]
            {
                new Fleet { Code = "FL-001", Name = "Flota Principal", Description = "Operaciones regulares y servicios locales", Type = FleetType.Primary },
                new Fleet { Code = "FL-002", Name = "Flota Secundaria", Description = "Operaciones de apoyo", Type = FleetType.Secondary },
                new Fleet { Code = "FL-003", Name = "Flota Externa", Description = "Vehículos externos contratados", Type = FleetType.External }
            };
            
            await context.Fleets.AddRangeAsync(fleets);
            await context.SaveChangesAsync();
            
            // Seed Drivers
            var drivers = new[]
            {
                new Driver { Code = "DR-001", FirstName = "Carlos", LastName = "Méndez", LicenseNumber = "L12345678", LicenseExpiryDate = DateTime.UtcNow.AddYears(2), ExperienceYears = 4, Phone = "+51987654321", Email = "carlos@flota365.com" },
                new Driver { Code = "DR-002", FirstName = "Ana", LastName = "Martínez", LicenseNumber = "L87654321", LicenseExpiryDate = DateTime.UtcNow.AddYears(1), ExperienceYears = 3, Phone = "+51123456789", Email = "ana@flota365.com" },
                new Driver { Code = "DR-003", FirstName = "Roberto", LastName = "Torres", LicenseNumber = "L11223344", LicenseExpiryDate = DateTime.UtcNow.AddYears(3), ExperienceYears = 7, Phone = "+51555666777", Email = "roberto@flota365.com" },
                new Driver { Code = "DR-004", FirstName = "Laura", LastName = "González", LicenseNumber = "L44556677", LicenseExpiryDate = DateTime.UtcNow.AddMonths(6), ExperienceYears = 2, Phone = "+51999888777", Email = "laura@flota365.com" },
                new Driver { Code = "DR-005", FirstName = "Pedro", LastName = "Ramírez", LicenseNumber = "L99887766", LicenseExpiryDate = DateTime.UtcNow.AddYears(4), ExperienceYears = 6, Phone = "+51444555666", Email = "pedro@flota365.com" }
            };
            
            await context.Drivers.AddRangeAsync(drivers);
            await context.SaveChangesAsync();
            
            // Seed Vehicles
            var vehicles = new[]
            {
                new Vehicle { LicensePlate = "ABC-123", Brand = "Toyota", Model = "Hilux", Year = 2023, Mileage = 15240, Status = VehicleStatus.Active, FleetId = fleets[0].Id, DriverId = drivers[0].Id, LastServiceDate = DateTime.UtcNow.AddDays(-45), NextServiceDate = DateTime.UtcNow.AddDays(15) },
                new Vehicle { LicensePlate = "XYZ-789", Brand = "Ford", Model = "Ranger", Year = 2022, Mileage = 28750, Status = VehicleStatus.Active, FleetId = fleets[0].Id, DriverId = drivers[1].Id, LastServiceDate = DateTime.UtcNow.AddDays(-30), NextServiceDate = DateTime.UtcNow.AddDays(30) },
                new Vehicle { LicensePlate = "DEF-456", Brand = "Mitsubishi", Model = "L200", Year = 2023, Mileage = 9830, Status = VehicleStatus.OnRoute, FleetId = fleets[0].Id, DriverId = drivers[2].Id, LastServiceDate = DateTime.UtcNow.AddDays(-60), NextServiceDate = DateTime.UtcNow.AddDays(10) },
                new Vehicle { LicensePlate = "GHI-789", Brand = "Nissan", Model = "Frontier", Year = 2022, Mileage = 32450, Status = VehicleStatus.Maintenance, FleetId = fleets[0].Id, DriverId = null, LastServiceDate = DateTime.UtcNow.AddDays(-5), NextServiceDate = DateTime.UtcNow.AddDays(5) },
                new Vehicle { LicensePlate = "JKL-012", Brand = "Toyota", Model = "Hilux", Year = 2021, Mileage = 45670, Status = VehicleStatus.Active, FleetId = fleets[0].Id, DriverId = drivers[4].Id, LastServiceDate = DateTime.UtcNow.AddDays(-20), NextServiceDate = DateTime.UtcNow.AddDays(40) }
            };
            
            await context.Vehicles.AddRangeAsync(vehicles);
            await context.SaveChangesAsync();
        }
    }
}