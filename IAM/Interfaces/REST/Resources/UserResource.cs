namespace Flota365.Platform.API.IAM.Interfaces.REST.Resources
{
    public record UserResource(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string Role,
        bool IsActive,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}