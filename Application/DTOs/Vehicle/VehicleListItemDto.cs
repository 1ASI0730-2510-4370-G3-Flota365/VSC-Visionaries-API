using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Vehicle
{
    /// <summary>
    /// Lightweight DTO for vehicle list displays
    /// </summary>
    public class VehicleListItemDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public VehicleStatus Status { get; set; }
        public string StatusName => Status.ToString();
        public string? DriverName { get; set; }
        public bool IsServiceDue { get; set; }
    }
}