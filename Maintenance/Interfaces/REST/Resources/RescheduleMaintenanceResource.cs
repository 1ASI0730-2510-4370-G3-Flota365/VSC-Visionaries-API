namespace Flota365.Platform.API.Maintenance.Interfaces.REST.Resources
{
    public record RescheduleMaintenanceResource(
        DateTime NewScheduledDate,
        string Reason
    );
}
