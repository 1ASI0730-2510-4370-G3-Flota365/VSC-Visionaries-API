using Flota365.API.Domain.Entities;
using Flota365.API.Domain.Interfaces;
using Flota365.API.Application.DTOs.Driver; // CORREGIDO: era 'Drivers'
using Flota365.API.Domain.Enums;

namespace Flota365.API.Application.Services
{
    public class DriverService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DriverService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DriverDto>> GetAllDriversAsync()
        {
            var drivers = await _unitOfWork.Drivers.GetAllAsync();
            return drivers.Select(MapToDto);
        }

        public async Task<DriverDto?> GetDriverByIdAsync(int id)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            return driver != null ? MapToDto(driver) : null;
        }

        public async Task<DriverDto> CreateDriverAsync(CreateDriverDto dto)
        {
            // Check if license number already exists
            if (await _unitOfWork.Drivers.LicenseNumberExistsAsync(dto.LicenseNumber))
                throw new InvalidOperationException("License number already exists");

            var driver = new Driver
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                LicenseNumber = dto.LicenseNumber,
                LicenseExpiryDate = dto.LicenseExpiryDate,
                Phone = dto.Phone,
                Email = dto.Email,
                ExperienceYears = dto.ExperienceYears,
                Status = DriverStatus.Active
            };

            await _unitOfWork.Drivers.CreateAsync(driver);
            await _unitOfWork.SaveChangesAsync();
            
            return MapToDto(driver);
        }

        public async Task<DriverDto?> UpdateDriverAsync(int id, UpdateDriverDto dto)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            if (driver == null) return null;

            // Check if license number already exists (excluding current driver)
            if (await _unitOfWork.Drivers.LicenseNumberExistsAsync(dto.LicenseNumber, id))
                throw new InvalidOperationException("License number already exists");

            driver.FirstName = dto.FirstName;
            driver.LastName = dto.LastName;
            driver.LicenseNumber = dto.LicenseNumber;
            driver.LicenseExpiryDate = dto.LicenseExpiryDate;
            driver.Phone = dto.Phone;
            driver.Email = dto.Email;
            driver.ExperienceYears = dto.ExperienceYears;
            driver.Status = dto.Status;
            driver.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Drivers.UpdateAsync(driver);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(driver);
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            if (driver == null) return false;

            // Check if driver has assigned vehicles
            if (driver.Vehicles.Any(v => v.IsActive))
                throw new InvalidOperationException("Cannot delete driver with assigned vehicles");

            await _unitOfWork.Drivers.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<DriverStatsDto> GetDriverStatsAsync()
        {
            var allDrivers = await _unitOfWork.Drivers.GetAllAsync();
            var totalDrivers = allDrivers.Count();
            var activeDrivers = await _unitOfWork.Drivers.GetActiveDriversCountAsync();
            var licensesExpiringSoon = await _unitOfWork.Drivers.GetLicensesExpiringSoonCountAsync();
            
            // Calculate additional stats
            var inactiveDrivers = allDrivers.Count(d => !d.IsActive);
            var driversOnRoute = allDrivers.Count(d => d.Status == DriverStatus.OnRoute);
            var expiredLicenses = allDrivers.Count(d => d.LicenseExpiryDate <= DateTime.UtcNow);
            var averageExperience = totalDrivers > 0 ? allDrivers.Average(d => d.ExperienceYears) : 0;

            return new DriverStatsDto
            {
                TotalDrivers = totalDrivers,
                ActiveDrivers = activeDrivers,
                InactiveDrivers = inactiveDrivers,
                DriversOnRoute = driversOnRoute,
                LicensesExpiringSoon = licensesExpiringSoon,
                ExpiredLicenses = expiredLicenses,
                AverageExperience = Math.Round(averageExperience, 1),
                AverageDistance = 753 // Mock data - implement based on actual requirements
            };
        }

        private static DriverDto MapToDto(Driver driver)
        {
            return new DriverDto
            {
                Id = driver.Id,
                Code = driver.Code,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                // FullName e IsLicenseExpiringSoon son computed properties, no se asignan
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                Phone = driver.Phone,
                Email = driver.Email,
                ExperienceYears = driver.ExperienceYears,
                Status = driver.Status,
                AssignedVehicle = driver.Vehicles.FirstOrDefault(v => v.IsActive) != null 
                    ? $"{driver.Vehicles.First(v => v.IsActive).Brand} {driver.Vehicles.First(v => v.IsActive).Model} ({driver.Vehicles.First(v => v.IsActive).LicensePlate})"
                    : null,
                IsActive = driver.IsActive,
                CreatedAt = driver.CreatedAt,
                UpdatedAt = driver.UpdatedAt
                // IsLicenseExpiringSoon se calcula automáticamente
            };
        }
    }
}