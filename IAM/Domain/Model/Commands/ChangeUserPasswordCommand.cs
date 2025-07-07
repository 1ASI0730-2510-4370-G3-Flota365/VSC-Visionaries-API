using Flota365.Platform.API.Shared.Domain.Model;


namespace Flota365.Platform.API.IAM.Domain.Model.Commands
{
    public record ChangeUserPasswordCommand(
        int UserId,
        string CurrentPassword,
        string NewPassword
    ) : ICommand<bool>;
}