using MediatR;
using OneAccount.Domain.Abstraction.Events;

namespace OneAccount.Infrastructure.Data.DomainEvents.Publishers;

public sealed class MediatRDomainEventPublisher
{
    private readonly IPublisher _publisher;

    public MediatRDomainEventPublisher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var notification = CreateNotification(domainEvent);

        await _publisher.Publish(notification, cancellationToken);
    }

    private static INotification CreateNotification(IDomainEvent domainEvent)
    {
        var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());

        return (INotification)Activator.CreateInstance(notificationType, domainEvent)!;
    }
}