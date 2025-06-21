// UpdateMaintenanceDto.cs
using System.ComponentModel.DataAnnotations;
using Flota365.API.Domain.Enums; // CORREGIDO: era 'Flota365.API.Models.Enums'

namespace Flota365.API.Application.DTOs.Maintenance
{
    /// <summary>
    /// DTO for updating maintenance record
    /// </summary>
    public class UpdateMaintenanceDto
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Maintenance type is required")]
        public MaintenanceType Type { get; set; } // CORREGIDO
        
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal Cost { get; set; }
        
        [Required(ErrorMessage = "Scheduled date is required")]
        public DateTime ScheduledDate { get; set; }
        
        public DateTime? CompletedDate { get; set; }
        
        [Required(ErrorMessage = "Status is required")]
        public MaintenanceStatus Status { get; set; } // CORREGIDO
        
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}