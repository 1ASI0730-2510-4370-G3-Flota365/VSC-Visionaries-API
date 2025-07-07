namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources
{
    public record UpdateFleetResource(
        string Name,
        string Description,
        string Type,
        bool IsActive
    );
}
