using Flota365.Platform.API.Shared.Domain.Model.Events;

namespace Flota365.Platform.API.FleetManagement.Domain.Model.Events
{
    public record VehicleCreatedEvent(
        int VehicleId,
        string LicensePlate,
        string Brand,
        string Model,
        DateTime CreatedAt
    ) : DomainEvent;
}
