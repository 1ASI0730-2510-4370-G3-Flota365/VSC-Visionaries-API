// CreateMaintenanceDto.cs
using System.ComponentModel.DataAnnotations;
using Flota365.API.Domain.Enums; // CORREGIDO: era 'Flota365.API.Models.Enums'

namespace Flota365.API.Application.DTOs.Maintenance
{
    /// <summary>
    /// DTO for creating a new maintenance record
    /// </summary>
    public class CreateMaintenanceDto
    {
        [Required(ErrorMessage = "Vehicle ID is required")]
        public int VehicleId { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Maintenance type is required")]
        public MaintenanceType Type { get; set; } // CORREGIDO
        
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal Cost { get; set; } = 0;
        
        [Required(ErrorMessage = "Scheduled date is required")]
        public DateTime ScheduledDate { get; set; }
        
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}