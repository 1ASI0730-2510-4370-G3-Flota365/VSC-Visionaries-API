namespace Flota365.Platform.API.IAM.Interfaces.REST.Resources
{
    public record UpdateProfileResource(
        string FirstName,
        string LastName,
        string Email
    );
}