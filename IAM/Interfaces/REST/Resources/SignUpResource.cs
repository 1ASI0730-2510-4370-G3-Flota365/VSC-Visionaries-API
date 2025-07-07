namespace Flota365.Platform.API.IAM.Interfaces.REST.Resources
{
    public record SignUpResource(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Role = "Administrator"
    );
}