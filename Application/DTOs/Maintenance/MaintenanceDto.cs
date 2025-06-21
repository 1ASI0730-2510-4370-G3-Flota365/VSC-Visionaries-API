// MaintenanceDto.cs
using Flota365.API.Domain.Enums; // CORREGIDO: era 'Flota365.API.Models.Enums'

namespace Flota365.API.Application.DTOs.Maintenance
{
    /// <summary>
    /// DTO for maintenance record information
    /// </summary>
    public class MaintenanceDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLicensePlate { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public MaintenanceType Type { get; set; } // CORREGIDO
        public string TypeName => Type.ToString();
        public decimal Cost { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public MaintenanceStatus Status { get; set; } // CORREGIDO
        public string StatusName => Status.ToString();
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Computed properties
        public bool IsOverdue => Status != MaintenanceStatus.Completed && // CORREGIDO
                        ScheduledDate < DateTime.UtcNow;
        public int DaysOverdue => IsOverdue ? 
            (int)(DateTime.UtcNow - ScheduledDate).TotalDays : 0;
    }
}