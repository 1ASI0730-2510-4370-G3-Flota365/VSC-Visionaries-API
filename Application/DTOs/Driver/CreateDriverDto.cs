using System.ComponentModel.DataAnnotations;

namespace Flota365.API.Application.DTOs.Driver
{
    /// <summary>
    /// DTO for creating a new driver
    /// </summary>
    public class CreateDriverDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "License number is required")]
        [StringLength(50, ErrorMessage = "License number cannot exceed 50 characters")]
        public string LicenseNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "License expiry date is required")]
        [DataType(DataType.Date)]
        public DateTime LicenseExpiryDate { get; set; }
        
        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string Phone { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string Email { get; set; } = string.Empty;
        
        [Range(0, 50, ErrorMessage = "Experience years must be between 0 and 50")]
        public int ExperienceYears { get; set; } = 0;
        
        // Custom validation
        public bool IsValidLicenseExpiryDate()
        {
            return LicenseExpiryDate > DateTime.UtcNow;
        }
    }
}