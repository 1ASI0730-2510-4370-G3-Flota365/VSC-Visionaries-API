namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform
{
    public static class CreateVehicleCommandFromResourceAssembler
    {
        public static CreateVehicleCommand ToCommandFromResource(CreateVehicleResource resource)
        {
            return new CreateVehicleCommand(
                resource.LicensePlate,
                resource.Brand,
                resource.Model,
                resource.Year,
                resource.Mileage,
                resource.FleetId,
                resource.DriverId
            );
        }
    }
}
