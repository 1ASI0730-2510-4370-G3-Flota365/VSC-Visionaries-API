namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources
{
    public record CreateFleetResource(
        string Code,
        string Name,
        string Description,
        string Type
    );
}
