using OneAccount.Domain.Abstraction;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Infrastructure.Data.Context;
using OneAccount.Infrastructure.Data.DomainEvents.Dispatchers;

namespace OneAccount.Infrastructure.Data;

public sealed class UnityOfWork : IUnityOfWork
{
    private readonly AppDbContext _context;
    private readonly DomainEventDispatcher _domainEventDispatcher;

    public UnityOfWork(
        AppDbContext context,
        DomainEventDispatcher domainEventDispatcher)
    {
        _context = context;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        var entitiesWithEvents = _context.ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .Where(entity => entity.DomainEvents.Any())
            .ToList();

        var domainEvents = entitiesWithEvents
            .SelectMany(entity => entity.DomainEvents)
            .ToList();

        await _context.SaveChangesAsync(cancellationToken);

        if (domainEvents.Count == 0)
            return;

        await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);

        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }
    }
}
