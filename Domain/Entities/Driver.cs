using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Flota365.API.Domain.Enums; // CORREGIDO: era 'Flota365.API.Models.Enums'

namespace Flota365.API.Domain.Entities
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; } = string.Empty;
        
        public DateTime LicenseExpiryDate { get; set; }
        
        [StringLength(15)]
        public string Phone { get; set; } = string.Empty;
        
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        
        public int ExperienceYears { get; set; } = 0;
        
        public DriverStatus Status { get; set; } = DriverStatus.Active;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        
        [NotMapped]
        public bool IsLicenseExpiringSoon => LicenseExpiryDate <= DateTime.UtcNow.AddDays(30);
        
        // Navigation properties
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}