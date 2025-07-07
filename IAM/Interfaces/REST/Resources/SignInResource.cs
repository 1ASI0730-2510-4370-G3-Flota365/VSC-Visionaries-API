namespace Flota365.Platform.API.IAM.Interfaces.REST.Resources
{
    public record SignInResource(
        string Email,
        string Password
    );
}