namespace OneAccount.Domain.Abstraction.Events;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}