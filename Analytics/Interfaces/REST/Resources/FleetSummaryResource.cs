namespace Flota365.Platform.API.Analytics.Interfaces.REST.Resources
{
    public record FleetSummaryResource(
        int TotalFleets,
        int PrimaryFleetVehicles,
        int SecondaryFleetVehicles,
        int ExternalFleetVehicles,
        double PrimaryFleetEfficiency,
        double SecondaryFleetEfficiency,
        double ExternalFleetEfficiency,
        double OverallEfficiency,
        string PrimaryFleetTrend,
        string SecondaryFleetTrend,
        string ExternalFleetTrend
    );
}