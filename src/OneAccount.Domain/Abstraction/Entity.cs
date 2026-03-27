using OneAccount.Domain.Abstraction.Events;

namespace OneAccount.Domain.Abstraction;

public abstract class Entity : IEquatable<Entity>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public Guid Id { get; protected set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (GetType() != other.GetType())
        {
            return false;
        }

        // Entidades "transientes" (sem Id atribuído) não são consideradas iguais
        if (Id == Guid.Empty || other.Id == Guid.Empty)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override bool Equals(object? obj) => Equals(obj as Entity);

    public override int GetHashCode()
    {
        // Para entidades persistidas, usa tipo+Id para reduzir colisões entre tipos.
        // Para transientes, fallback ao hash de referência evita comportamento inesperado.
        return Id == Guid.Empty ? base.GetHashCode() : HashCode.Combine(GetType(), Id);
    }

    public static bool operator ==(Entity? left, Entity? right) =>
        left is null ? right is null : left.Equals(right);

    public static bool operator !=(Entity? left, Entity? right) => !(left == right);
}
