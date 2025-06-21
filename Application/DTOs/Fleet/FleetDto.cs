using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Fleet
{
    /// <summary>
    /// DTO for fleet information display
    /// </summary>
    public class FleetDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public FleetType Type { get; set; }
        public string TypeName => Type.ToString();
        public bool IsActive { get; set; }
        public int VehicleCount { get; set; }
        public int ActiveVehicles { get; set; }
        public int InMaintenanceVehicles { get; set; }
        public double Performance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Computed properties
        public double PerformancePercentage => Math.Round(Performance, 1);
        public string StatusText => IsActive ? "Active" : "Inactive";
        public double VehicleUtilization => VehicleCount > 0 ? 
            Math.Round((double)ActiveVehicles / VehicleCount * 100, 1) : 0;
    }
}