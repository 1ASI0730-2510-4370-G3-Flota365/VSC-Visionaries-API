namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform
{
    public static class UpdateMileageCommandFromResourceAssembler
    {
        public static UpdateVehicleMileageCommand ToCommandFromResource(int vehicleId, UpdateMileageResource resource)
        {
            return new UpdateVehicleMileageCommand(vehicleId, resource.NewMileage);
        }
    }
}
