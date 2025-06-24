using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flota365.API.Domain.Entities
{
    public class ServiceRecord
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int VehicleId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ServiceType { get; set; } = string.Empty; // Oil Change, Tire Rotation, etc.
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal Cost { get; set; } = 0;
        
        public DateTime ServiceDate { get; set; }
        public int MileageAtService { get; set; }
        
        [StringLength(200)]
        public string ServiceProvider { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}