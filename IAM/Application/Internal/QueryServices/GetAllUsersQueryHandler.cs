using MediatR;
using Flota365.Platform.API.IAM.Domain.Model.Queries;
using Flota365.Platform.API.IAM.Domain.Repositories;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;
using Flota365.Platform.API.IAM.Interfaces.REST.Transform;


// GetAllUsersQueryHandler.cs
namespace Flota365.Platform.API.IAM.Application.Internal.QueryServices
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResource>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResource>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync();
            return users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        }
    }
}