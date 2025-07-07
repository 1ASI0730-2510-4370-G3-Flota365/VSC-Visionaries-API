using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.FleetManagement.Domain.Model.ValueObjects;

namespace Flota365.Platform.API.FleetManagement.Domain.Model.Commands
{
    public record UpdateVehicleCommand(
        int VehicleId,
        string LicensePlate,
        string Brand,
        string Model,
        int Year,
        int Mileage,
        VehicleStatus Status,
        int? FleetId,
        int? DriverId,
        DateTime? LastServiceDate,
        DateTime? NextServiceDate
    ) : ICommand<bool>;
}
