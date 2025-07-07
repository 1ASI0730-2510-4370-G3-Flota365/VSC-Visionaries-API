namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform
{
    public static class UpdateVehicleCommandFromResourceAssembler
    {
        public static UpdateVehicleCommand ToCommandFromResource(int vehicleId, UpdateVehicleResource resource)
        {
            if (!Enum.TryParse<VehicleStatus>(resource.Status, true, out var status))
                throw new ArgumentException($"Invalid vehicle status: {resource.Status}");

            return new UpdateVehicleCommand(
                vehicleId,
                resource.LicensePlate,
                resource.Brand,
                resource.Model,
                resource.Year,
                resource.Mileage,
                status,
                resource.FleetId,
                resource.DriverId,
                resource.LastServiceDate,
                resource.NextServiceDate
            );
        }
    }
}
