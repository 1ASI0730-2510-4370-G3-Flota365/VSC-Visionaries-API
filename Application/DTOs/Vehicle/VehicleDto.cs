using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Vehicle
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Mileage { get; set; }
        public VehicleStatus Status { get; set; }
        public string StatusName => Status.ToString();
        public int? FleetId { get; set; }
        public string? FleetName { get; set; }
        public int? DriverId { get; set; }
        public string? DriverName { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}