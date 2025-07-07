using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Personnel.Domain.Model.ValueObjects;

namespace Flota365.Platform.API.Personnel.Domain.Model.Commands
{
    public record UpdateDriverCommand(
        int DriverId,
        string FirstName,
        string LastName,
        string LicenseNumber,
        DateTime LicenseExpiryDate,
        string Phone,
        string Email,
        int ExperienceYears,
        DriverStatus Status
    ) : ICommand<bool>;
}
