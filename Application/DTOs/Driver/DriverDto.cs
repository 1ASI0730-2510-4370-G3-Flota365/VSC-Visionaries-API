using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Driver
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        
        // CORREGIDO: Volver a computed property (read-only)
        public string FullName => $"{FirstName} {LastName}";
        
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime LicenseExpiryDate { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public DriverStatus Status { get; set; }
        public string StatusName => Status.ToString();
        public string? AssignedVehicle { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // CORREGIDO: Volver a computed property (read-only)
        public bool IsLicenseExpiringSoon => LicenseExpiryDate <= DateTime.UtcNow.AddDays(30);
    }
}