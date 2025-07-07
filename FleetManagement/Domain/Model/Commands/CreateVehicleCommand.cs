using Flota365.Platform.API.Shared.Domain.Model;

namespace Flota365.Platform.API.FleetManagement.Domain.Model.Commands
{
    public record CreateVehicleCommand(
        string LicensePlate,
        string Brand, 
        string Model,
        int Year,
        int Mileage,
        int? FleetId,
        int? DriverId
    ) : ICommand<int>;
}
