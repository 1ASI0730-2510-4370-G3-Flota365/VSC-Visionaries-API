using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flota365.API.Domain.Entities
{
    public class MaintenanceRecord
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int VehicleId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        
        public MaintenanceType Type { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal Cost { get; set; } = 0;
        
        public DateTime ScheduledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        
        public MaintenanceStatus Status { get; set; } = MaintenanceStatus.Scheduled;
        
        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
    
    public enum MaintenanceType
    {
        Preventive = 1,
        Corrective = 2,
        Emergency = 3
    }
    
    public enum MaintenanceStatus
    {
        Scheduled = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }
}