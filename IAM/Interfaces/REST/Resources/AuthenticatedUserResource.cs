namespace Flota365.Platform.API.IAM.Interfaces.REST.Resources
{
    public record AuthenticatedUserResource(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string Role,
        string Token,
        DateTime ExpiresAt
    );
}