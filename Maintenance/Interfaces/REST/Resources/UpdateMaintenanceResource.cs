namespace Flota365.Platform.API.Maintenance.Interfaces.REST.Resources
{
    public record UpdateMaintenanceResource(
        string Description,
        string Type,
        decimal EstimatedCost,
        DateTime ScheduledDate,
        string ServiceProvider,
        string Notes,
        string Status
    );
}
