namespace OneAccount.Domain.Abstraction.Events;

public interface IDomainEvent
{
    DateTimeOffset OccurredOnUtc { get; }
}