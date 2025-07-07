using MediatR;
using Flota365.Platform.API.IAM.Domain.Model.Commands;
using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.IAM.Domain.Repositories;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.IAM.Application.Internal.CommandServices
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.UserId);
            if (user == null || !user.IsActive)
                return false;

            // Verificar contraseña actual
            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                return false;

            // Cambiar contraseña
            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.ChangePassword(newPasswordHash);
            
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}