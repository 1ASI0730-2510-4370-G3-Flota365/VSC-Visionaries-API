using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Flota365.API.Domain.Enums; // CORREGIDO: era 'Flota365.API.Models.Enums'

namespace Flota365.API.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string LicensePlate { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;
        
        public int Year { get; set; }
        
        public int Mileage { get; set; } = 0;
        
        public VehicleStatus Status { get; set; } = VehicleStatus.Active;
        
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Keys
        public int? FleetId { get; set; }
        public int? DriverId { get; set; }
        
        // Navigation properties
        [ForeignKey("FleetId")]
        public virtual Fleet? Fleet { get; set; }
        
        [ForeignKey("DriverId")]
        public virtual Driver? Driver { get; set; }
        
        // Collections
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
        public virtual ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();
    }
}