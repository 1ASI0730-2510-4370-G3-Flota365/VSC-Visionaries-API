namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform
{
    public static class UpdateFleetCommandFromResourceAssembler
    {
        public static UpdateFleetCommand ToCommandFromResource(int fleetId, UpdateFleetResource resource)
        {
            if (!Enum.TryParse<Domain.Model.ValueObjects.FleetType>(resource.Type, true, out var fleetType))
                throw new ArgumentException($"Invalid fleet type: {resource.Type}");

            return new UpdateFleetCommand(
                fleetId,
                resource.Name,
                resource.Description,
                fleetType,
                resource.IsActive
            );
        }
    }
}
