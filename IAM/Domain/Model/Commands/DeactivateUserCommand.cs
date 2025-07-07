using Flota365.Platform.API.Shared.Domain.Model;


namespace Flota365.Platform.API.IAM.Domain.Model.Commands
{
    public record DeactivateUserCommand(
        int UserId
    ) : ICommand<bool>;
}