using MediatR;
namespace Flota365.Platform.API.Shared.Domain.Model.Events
{
    public abstract record DomainEvent(DateTime OccurredOn) : INotification
    {
        protected DomainEvent() : this(DateTime.UtcNow) { }
        public Guid EventId { get; } = Guid.NewGuid();
        public string EventType => GetType().Name;
        public int Version { get; init; } = 1;
    }
}
