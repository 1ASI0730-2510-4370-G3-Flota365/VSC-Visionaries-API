using MediatR;
using Flota365.Platform.API.IAM.Domain.Model.Queries;
using Flota365.Platform.API.IAM.Domain.Repositories;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;
using Flota365.Platform.API.IAM.Interfaces.REST.Transform;



// GetUserByEmailQueryHandler.cs
namespace Flota365.Platform.API.IAM.Application.Internal.QueryServices
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserResource?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResource?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);
            return user != null ? UserResourceFromEntityAssembler.ToResourceFromEntity(user) : null;
        }
    }
}