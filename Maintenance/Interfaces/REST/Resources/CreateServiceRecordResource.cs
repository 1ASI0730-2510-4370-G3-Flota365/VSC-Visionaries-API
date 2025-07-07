namespace Flota365.Platform.API.Maintenance.Interfaces.REST.Resources
{
    public record CreateServiceRecordResource(
        int VehicleId,
        string ServiceType,
        string Description,
        decimal Cost,
        DateTime ServiceDate,
        int MileageAtService,
        string ServiceProvider,
        string TechnicianName,
        string PartsUsed,
        string Notes
    );
}
