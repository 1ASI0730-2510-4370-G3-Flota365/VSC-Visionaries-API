using MediatR;
using Flota365.Platform.API.IAM.Domain.Model.Queries;
using Flota365.Platform.API.IAM.Domain.Repositories;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;
using Flota365.Platform.API.IAM.Interfaces.REST.Transform;


// GetActiveUsersQueryHandler.cs
namespace Flota365.Platform.API.IAM.Application.Internal.QueryServices
{
    public class GetActiveUsersQueryHandler : IRequestHandler<GetActiveUsersQuery, IEnumerable<UserResource>>
    {
        private readonly IUserRepository _userRepository;

        public GetActiveUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResource>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.FindActiveUsersAsync();
            return users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        }
    }
}