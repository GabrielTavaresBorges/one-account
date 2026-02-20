namespace OneAccount.Domain.Abstraction;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        Id = id;
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
