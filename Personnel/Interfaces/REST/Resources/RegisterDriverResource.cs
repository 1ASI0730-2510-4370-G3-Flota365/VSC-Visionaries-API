namespace Flota365.Platform.API.Personnel.Interfaces.REST.Resources
{
    public record RegisterDriverResource(
        string Code,
        string FirstName,
        string LastName,
        string LicenseNumber,
        DateTime LicenseExpiryDate,
        string Phone,
        string Email,
        int ExperienceYears
    );
}
