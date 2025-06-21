namespace Flota365.API.Application.DTOs.Driver
{
    /// <summary>
    /// DTO for driver statistics
    /// </summary>
    public class DriverStatsDto
    {
        public int TotalDrivers { get; set; }
        public int ActiveDrivers { get; set; }
        public int InactiveDrivers { get; set; }
        public int DriversOnRoute { get; set; }
        public int LicensesExpiringSoon { get; set; }
        public int ExpiredLicenses { get; set; }
        public double AverageExperience { get; set; }
        public double AverageDistance { get; set; }
        
        // Computed properties
        public double ActivePercentage => TotalDrivers > 0 ? 
            Math.Round((double)ActiveDrivers / TotalDrivers * 100, 1) : 0;
        public double LicenseCompliancePercentage => TotalDrivers > 0 ? 
            Math.Round((double)(TotalDrivers - ExpiredLicenses) / TotalDrivers * 100, 1) : 0;
    }
}