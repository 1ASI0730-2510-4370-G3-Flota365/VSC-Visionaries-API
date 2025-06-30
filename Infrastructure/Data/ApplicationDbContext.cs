using Microsoft.EntityFrameworkCore;
using Flota365.API.Domain.Entities;

namespace Flota365.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Fleet> Fleets { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
                
            modelBuilder.Entity<Fleet>()
                .HasIndex(f => f.Code)
                .IsUnique();
                
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.LicensePlate)
                .IsUnique();
                
            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.Code)
                .IsUnique();
                
            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.LicenseNumber)
                .IsUnique();
            
            // Configure relationships
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Fleet)
                .WithMany(f => f.Vehicles)
                .HasForeignKey(v => v.FleetId)
                .OnDelete(DeleteBehavior.SetNull);
                
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Driver)
                .WithMany(d => d.Vehicles)
                .HasForeignKey(v => v.DriverId)
                .OnDelete(DeleteBehavior.SetNull);
                
            modelBuilder.Entity<MaintenanceRecord>()
                .HasOne(m => m.Vehicle)
                .WithMany(v => v.MaintenanceRecords)
                .HasForeignKey(m => m.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<ServiceRecord>()
                .HasOne(s => s.Vehicle)
                .WithMany(v => v.ServiceRecords)
                .HasForeignKey(s => s.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}