namespace Flota365.Platform.API.Analytics.Interfaces.REST.Resources
{
    public record ActiveVehicleResource(
        int Id,
        string LicensePlate,
        string Model,
        string DriverName,
        string Status,
        string FleetName,
        DateTime LastUpdate
    );
}