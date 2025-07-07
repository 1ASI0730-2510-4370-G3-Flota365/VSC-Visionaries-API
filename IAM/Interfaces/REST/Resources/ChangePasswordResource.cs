namespace Flota365.Platform.API.IAM.Interfaces.REST.Resources
{
    public record ChangePasswordResource(
        string CurrentPassword,
        string NewPassword,
        string ConfirmPassword
    );
}