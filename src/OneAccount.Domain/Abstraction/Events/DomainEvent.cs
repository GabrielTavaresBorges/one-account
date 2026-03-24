namespace OneAccount.Domain.Abstraction.Events;

public abstract record DomainEvent : IDomainEvent
{
    public DateTimeOffset OccurredOnUtc { get; init; } = DateTime.UtcNow;
}