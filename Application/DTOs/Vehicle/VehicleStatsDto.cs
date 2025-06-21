namespace Flota365.API.Application.DTOs.Vehicle
{
    /// <summary>
    /// DTO for vehicle statistics
    /// </summary>
    public class VehicleStatsDto
    {
        public int TotalVehicles { get; set; }
        public int ActiveVehicles { get; set; }
        public int InMaintenanceVehicles { get; set; }
        public int InactiveVehicles { get; set; }
        public double AverageMileage { get; set; }
        public int VehiclesDueForService { get; set; }
        public double FleetEfficiency { get; set; }
        
        // Computed properties
        public double ActivePercentage => TotalVehicles > 0 ? 
            Math.Round((double)ActiveVehicles / TotalVehicles * 100, 1) : 0;
        public double MaintenancePercentage => TotalVehicles > 0 ? 
            Math.Round((double)InMaintenanceVehicles / TotalVehicles * 100, 1) : 0;
    }
}