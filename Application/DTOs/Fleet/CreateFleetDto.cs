using System.ComponentModel.DataAnnotations;
using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Fleet
{
    /// <summary>
    /// DTO for creating a new fleet
    /// </summary>
    public class CreateFleetDto
    {
        [Required(ErrorMessage = "Fleet name is required")]
        [StringLength(100, ErrorMessage = "Fleet name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Fleet type is required")]
        public FleetType Type { get; set; }
    }
}