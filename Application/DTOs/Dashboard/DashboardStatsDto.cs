namespace Flota365.API.Application.DTOs.Dashboard
{
    /// <summary>
    /// DTO for main dashboard statistics
    /// </summary>
    public class DashboardStatsDto
    {
        public int TotalVehicles { get; set; }
        public int ActiveDrivers { get; set; }
        public int VehiclesInMaintenance { get; set; }
        public double FleetEfficiency { get; set; }
        public string TotalVehiclesChange { get; set; } = string.Empty;
        public string ActiveDriversChange { get; set; } = string.Empty;
        public string MaintenanceChange { get; set; } = string.Empty;
        public string EfficiencyChange { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        
        // Additional metrics
        public int TotalFleets { get; set; }
        public int AlertsCount { get; set; }
        public double AverageVehicleAge { get; set; }
        public int VehiclesDueForService { get; set; }
    }
}