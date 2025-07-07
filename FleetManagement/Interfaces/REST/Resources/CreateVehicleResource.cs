namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources
{
    public record CreateVehicleResource(
        string LicensePlate,
        string Brand,
        string Model,
        int Year,
        int Mileage,
        int? FleetId,
        int? DriverId
    );
}
