using OneAccount.Domain.Abstraction.Events;
using OneAccount.Infrastructure.Data.DomainEvents.Publishers;

namespace OneAccount.Infrastructure.Data.DomainEvents.Dispatchers;

public sealed class DomainEventDispatcher
{
    private readonly MediatRDomainEventPublisher _publisher;

    public DomainEventDispatcher(MediatRDomainEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.PublishAsync(domainEvent, cancellationToken);
        }
    }
}
