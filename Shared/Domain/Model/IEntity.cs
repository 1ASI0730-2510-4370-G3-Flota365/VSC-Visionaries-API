namespace Flota365.Platform.API.Shared.Domain.Model
{
    public interface IEntity
    {
        int Id { get; }
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
    }
}
