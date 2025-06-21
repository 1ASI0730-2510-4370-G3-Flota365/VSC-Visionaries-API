using System.ComponentModel.DataAnnotations;
using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.DTOs.Fleet
{
    /// <summary>
    /// DTO for updating fleet information
    /// </summary>
    public class UpdateFleetDto : CreateFleetDto
    {
        public bool IsActive { get; set; } = true;
    }
}