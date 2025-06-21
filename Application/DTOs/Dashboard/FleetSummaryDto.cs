namespace Flota365.API.Application.DTOs.Dashboard
{
    /// <summary>
    /// DTO for fleet summary on dashboard
    /// </summary>
    public class FleetSummaryDto
    {
        public int TotalFleets { get; set; }
        public int PrimaryFleetVehicles { get; set; }
        public int SecondaryFleetVehicles { get; set; }
        public int ExternalFleetVehicles { get; set; }
        public double PrimaryFleetEfficiency { get; set; }
        public double SecondaryFleetEfficiency { get; set; }
        public double ExternalFleetEfficiency { get; set; }
        public double OverallEfficiency { get; set; }
        
        // Trend data
        public string PrimaryFleetTrend { get; set; } = string.Empty;
        public string SecondaryFleetTrend { get; set; } = string.Empty;
        public string ExternalFleetTrend { get; set; } = string.Empty;
    }
}