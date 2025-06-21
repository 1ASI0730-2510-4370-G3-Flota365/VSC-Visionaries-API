using System.ComponentModel.DataAnnotations;

namespace Flota365.API.Application.DTOs.Maintenance
{
    /// <summary>
    /// DTO for creating a new service record
    /// </summary>
    public class CreateServiceRecordDto
    {
        [Required(ErrorMessage = "Vehicle ID is required")]
        public int VehicleId { get; set; }
        
        [Required(ErrorMessage = "Service type is required")]
        [StringLength(100, ErrorMessage = "Service type cannot exceed 100 characters")]
        public string ServiceType { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal Cost { get; set; } = 0;
        
        [Required(ErrorMessage = "Service date is required")]
        public DateTime ServiceDate { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a positive value")]
        public int MileageAtService { get; set; }
        
        [StringLength(200, ErrorMessage = "Service provider cannot exceed 200 characters")]
        public string ServiceProvider { get; set; } = string.Empty;
    }
}