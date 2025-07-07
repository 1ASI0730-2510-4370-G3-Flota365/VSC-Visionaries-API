using Flota365.Platform.API.Shared.Domain.Model;

namespace Flota365.Platform.API.IAM.Domain.Model.Commands
{
    public record UpdateUserProfileCommand(
        int UserId,
        string FirstName,
        string LastName,
        string Email
    ) : ICommand<bool>;
}