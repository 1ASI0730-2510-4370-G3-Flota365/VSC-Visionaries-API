namespace Flota365.Platform.API.Analytics.Interfaces.REST.Resources
{
    public record DashboardStatsResource(
        int TotalVehicles,
        int ActiveDrivers,
        int VehiclesInMaintenance,
        double FleetEfficiency,
        string TotalVehiclesChange,
        string ActiveDriversChange,
        string MaintenanceChange,
        string EfficiencyChange
    );
}