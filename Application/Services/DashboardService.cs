using Flota365.API.Domain.Interfaces;
using Flota365.API.Application.DTOs.Dashboard;
using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.Services
{
    public class DashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            var totalVehicles = vehicles.Count();
            var activeDrivers = await _unitOfWork.Drivers.GetActiveDriversCountAsync();
            var vehiclesInMaintenance = vehicles.Count(v => v.Status == VehicleStatus.Maintenance);
            
            // Calculate fleet efficiency
            var workingVehicles = totalVehicles - vehiclesInMaintenance;
            var fleetEfficiency = totalVehicles > 0 ? (double)workingVehicles / totalVehicles * 100 : 0;

            return new DashboardStatsDto
            {
                TotalVehicles = totalVehicles,
                ActiveDrivers = activeDrivers,
                VehiclesInMaintenance = vehiclesInMaintenance,
                FleetEfficiency = Math.Round(fleetEfficiency, 1),
                TotalVehiclesChange = "↑ 2% esta semana",
                ActiveDriversChange = "↓ 1% esta semana",
                MaintenanceChange = "↑ 3% esta semana",
                EfficiencyChange = "↑ 5% este mes"
            };
        }

        public async Task<List<ActiveVehicleDto>> GetActiveVehiclesAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            
            return vehicles.Take(10).Select(v => new ActiveVehicleDto
            {
                Id = v.Id, // AGREGADO: ID faltante
                LicensePlate = v.LicensePlate,
                Model = $"{v.Brand} {v.Model} {v.Year}",
                DriverName = v.Driver?.FullName ?? "No asignado",
                Status = v.Status, // CORREGIDO: usar el enum directamente
                FleetName = v.Fleet?.Name ?? "Sin flota", // AGREGADO: FleetName faltante
                LastUpdate = v.UpdatedAt // AGREGADO: LastUpdate faltante
            }).ToList();
        }

        public async Task<FleetSummaryDto> GetFleetSummaryAsync()
        {
            var fleets = await _unitOfWork.Fleets.GetAllAsync();

            var primaryFleet = fleets.FirstOrDefault(f => f.Type == FleetType.Primary);
            var secondaryFleet = fleets.FirstOrDefault(f => f.Type == FleetType.Secondary);
            var externalFleet = fleets.FirstOrDefault(f => f.Type == FleetType.External);

            var primaryEfficiency = CalculateFleetEfficiency(primaryFleet?.Vehicles.Where(v => v.IsActive).ToList());
            var secondaryEfficiency = CalculateFleetEfficiency(secondaryFleet?.Vehicles.Where(v => v.IsActive).ToList());
            var externalEfficiency = CalculateFleetEfficiency(externalFleet?.Vehicles.Where(v => v.IsActive).ToList());

            return new FleetSummaryDto
            {
                TotalFleets = fleets.Count(),
                PrimaryFleetVehicles = primaryFleet?.Vehicles.Count(v => v.IsActive) ?? 0,
                SecondaryFleetVehicles = secondaryFleet?.Vehicles.Count(v => v.IsActive) ?? 0,
                ExternalFleetVehicles = externalFleet?.Vehicles.Count(v => v.IsActive) ?? 0,
                PrimaryFleetEfficiency = primaryEfficiency,
                SecondaryFleetEfficiency = secondaryEfficiency,
                ExternalFleetEfficiency = externalEfficiency,
                OverallEfficiency = Math.Round((primaryEfficiency + secondaryEfficiency + externalEfficiency) / 3, 1), // AGREGADO: OverallEfficiency calculado
                // AGREGADOS: Trends faltantes
                PrimaryFleetTrend = "↑ 2%",
                SecondaryFleetTrend = "→ 0%", 
                ExternalFleetTrend = "↓ 1%"
            };
        }

        private static double CalculateFleetEfficiency(List<Domain.Entities.Vehicle>? vehicles)
        {
            if (vehicles == null || !vehicles.Any()) return 100;
            
            var workingVehicles = vehicles.Count(v => v.Status != VehicleStatus.Maintenance);
            return Math.Round((double)workingVehicles / vehicles.Count * 100, 1);
        }
    }
}