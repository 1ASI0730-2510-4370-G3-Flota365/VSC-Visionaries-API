using System.ComponentModel.DataAnnotations;
using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Vehicle
{
    /// <summary>
    /// DTO for updating vehicle information
    /// </summary>
    public class UpdateVehicleDto : CreateVehicleDto
    {
        [Required(ErrorMessage = "Status is required")]
        public VehicleStatus Status { get; set; }
        
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        
        // Custom validation
        public bool IsValidServiceDates()
        {
            if (LastServiceDate.HasValue && NextServiceDate.HasValue)
            {
                return NextServiceDate.Value > LastServiceDate.Value;
            }
            return true;
        }
    }
}