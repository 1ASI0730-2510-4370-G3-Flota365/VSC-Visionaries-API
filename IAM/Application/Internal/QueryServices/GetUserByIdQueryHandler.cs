using MediatR;
using Flota365.Platform.API.IAM.Domain.Model.Queries;
using Flota365.Platform.API.IAM.Domain.Repositories;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;
using Flota365.Platform.API.IAM.Interfaces.REST.Transform;

// GetUserByIdQueryHandler.cs
namespace Flota365.Platform.API.IAM.Application.Internal.QueryServices
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResource?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResource?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.UserId);
            return user != null ? UserResourceFromEntityAssembler.ToResourceFromEntity(user) : null;
        }
    }
}