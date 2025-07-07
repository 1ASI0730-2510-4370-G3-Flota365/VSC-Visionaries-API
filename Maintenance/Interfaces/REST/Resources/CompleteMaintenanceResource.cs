namespace Flota365.Platform.API.Maintenance.Interfaces.REST.Resources
{
    public record CompleteMaintenanceResource(
        decimal ActualCost,
        string CompletionNotes
    );
}
