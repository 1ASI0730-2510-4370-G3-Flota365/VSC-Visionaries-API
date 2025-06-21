namespace Flota365.API.Application.DTOs.Maintenance
{
    /// <summary>
    /// DTO for service record information
    /// </summary>
    public class ServiceRecordDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLicensePlate { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public DateTime ServiceDate { get; set; }
        public int MileageAtService { get; set; }
        public string ServiceProvider { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}