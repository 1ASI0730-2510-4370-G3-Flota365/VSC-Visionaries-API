namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources
{
    public record UpdateVehicleResource(
        string LicensePlate,
        string Brand,
        string Model,
        int Year,
        int Mileage,
        string Status,
        int? FleetId,
        int? DriverId,
        DateTime? LastServiceDate,
        DateTime? NextServiceDate
    );
}
