using MediatR;

namespace Flota365.Platform.API.Shared.Domain.Model
{
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
