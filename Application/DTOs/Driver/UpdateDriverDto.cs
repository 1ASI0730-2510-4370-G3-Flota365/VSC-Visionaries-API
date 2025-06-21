using System.ComponentModel.DataAnnotations;
using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Driver
{
    /// <summary>
    /// DTO for updating driver information
    /// </summary>
    public class UpdateDriverDto : CreateDriverDto
    {
        [Required(ErrorMessage = "Status is required")]
        public DriverStatus Status { get; set; }
    }
}