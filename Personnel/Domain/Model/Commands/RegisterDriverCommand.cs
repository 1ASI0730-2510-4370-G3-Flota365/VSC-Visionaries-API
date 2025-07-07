using Flota365.Platform.API.Shared.Domain.Model;

namespace Flota365.Platform.API.Personnel.Domain.Model.Commands
{
    public record RegisterDriverCommand(
        string Code,
        string FirstName,
        string LastName,
        string LicenseNumber,
        DateTime LicenseExpiryDate,
        string Phone,
        string Email,
        int ExperienceYears
    ) : ICommand<int>;
}
