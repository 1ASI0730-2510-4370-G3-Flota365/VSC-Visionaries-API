using MediatR;

namespace Flota365.Platform.API.Shared.Domain.Model
{
    public interface ICommand : IRequest<bool> { }
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}