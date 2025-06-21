using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Driver
{
    /// <summary>
    /// Lightweight DTO for driver list displays
    /// </summary>
    public class DriverListItemDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public DriverStatus Status { get; set; }
        public string StatusName => Status.ToString();
        public bool IsLicenseExpiringSoon { get; set; }
        public string? AssignedVehicle { get; set; }
    }
}