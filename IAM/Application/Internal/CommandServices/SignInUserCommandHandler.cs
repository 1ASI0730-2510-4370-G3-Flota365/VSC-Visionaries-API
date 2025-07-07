using MediatR;
using Flota365.Platform.API.IAM.Domain.Model.Commands;
using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.IAM.Domain.Repositories;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.IAM.Application.Internal.CommandServices
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public SignInUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);
            
            if (user == null || !user.IsActive)
                throw new UnauthorizedAccessException("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return user.Id;
        }
    }
}
