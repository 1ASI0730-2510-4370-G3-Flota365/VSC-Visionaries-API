using Flota365.API.Domain.Entities;
using Flota365.API.Domain.Interfaces;
using Flota365.API.Application.DTOs.Vehicle; // CORREGIDO: era 'Vehicles'
using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.Services
{
    public class VehicleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            return vehicles.Select(MapToDto);
        }

        public async Task<VehicleDto?> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            return vehicle != null ? MapToDto(vehicle) : null;
        }

        public async Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto dto)
        {
            // Check if license plate already exists
            if (await _unitOfWork.Vehicles.LicensePlateExistsAsync(dto.LicensePlate))
                throw new InvalidOperationException("License plate already exists");

            var vehicle = new Vehicle
            {
                LicensePlate = dto.LicensePlate,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                Mileage = dto.Mileage,
                FleetId = dto.FleetId,
                DriverId = dto.DriverId,
                Status = VehicleStatus.Active
            };

            await _unitOfWork.Vehicles.CreateAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();
            
            return MapToDto(vehicle);
        }

        public async Task<VehicleDto?> UpdateVehicleAsync(int id, UpdateVehicleDto dto)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null) return null;

            // Check if license plate already exists (excluding current vehicle)
            if (await _unitOfWork.Vehicles.LicensePlateExistsAsync(dto.LicensePlate, id))
                throw new InvalidOperationException("License plate already exists");

            vehicle.LicensePlate = dto.LicensePlate;
            vehicle.Brand = dto.Brand;
            vehicle.Model = dto.Model;
            vehicle.Year = dto.Year;
            vehicle.Mileage = dto.Mileage;
            vehicle.Status = dto.Status;
            vehicle.FleetId = dto.FleetId;
            vehicle.DriverId = dto.DriverId;
            vehicle.LastServiceDate = dto.LastServiceDate;
            vehicle.NextServiceDate = dto.NextServiceDate;
            vehicle.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Vehicles.UpdateAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(vehicle);
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null) return false;

            await _unitOfWork.Vehicles.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private static VehicleDto MapToDto(Vehicle vehicle)
        {
            return new VehicleDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Mileage = vehicle.Mileage,
                Status = vehicle.Status,
                FleetId = vehicle.FleetId,
                FleetName = vehicle.Fleet?.Name,
                DriverId = vehicle.DriverId,
                DriverName = vehicle.Driver?.FullName,
                LastServiceDate = vehicle.LastServiceDate,
                NextServiceDate = vehicle.NextServiceDate,
                CreatedAt = vehicle.CreatedAt,
                UpdatedAt = vehicle.UpdatedAt
            };
        }
    }
}