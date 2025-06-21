using System.ComponentModel.DataAnnotations;

namespace Flota365.API.Application.DTOs.Vehicle
{
    /// <summary>
    /// DTO for creating a new vehicle
    /// </summary>
    public class CreateVehicleDto
    {
        [Required(ErrorMessage = "License plate is required")]
        [StringLength(20, ErrorMessage = "License plate cannot exceed 20 characters")]
        public string LicensePlate { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Brand is required")]
        [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters")]
        public string Brand { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters")]
        public string Model { get; set; } = string.Empty;
        
        [Range(1900, 2030, ErrorMessage = "Year must be between 1900 and 2030")]
        public int Year { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a positive value")]
        public int Mileage { get; set; } = 0;
        
        public int? FleetId { get; set; }
        public int? DriverId { get; set; }
    }
}