using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Dashboard
{
    /// <summary>
    /// DTO for active vehicle display on dashboard
    /// </summary>
    public class ActiveVehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public VehicleStatus Status { get; set; }
        public string StatusName => Status.ToString();
        public string FleetName { get; set; } = string.Empty;
        public DateTime? LastUpdate { get; set; }
        
        // Visual indicators
        public string StatusColor => Status switch
        {
            VehicleStatus.Active => "green",
            VehicleStatus.OnRoute => "blue", 
            VehicleStatus.Maintenance => "orange",
            VehicleStatus.Inactive => "red",
            _ => "gray"
        };
    }
}