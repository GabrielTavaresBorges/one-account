using OneAccount.Domain.Abstraction;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.ValueObjects.Security;

namespace OneAccount.Domain.Entities.EmailConfirmationTokens;

public sealed class EmailConfirmationToken : Entity, IAggregateRoot
{
    private Guid _userId;
    private TokenHash _tokenHash = null!;
    private DateTimeOffset _createdAt;
    private DateTimeOffset _expiresAt;
    private DateTimeOffset? _usedAt;

    public Guid UserId => _userId;
    public TokenHash TokenHash => _tokenHash;
    public DateTimeOffset CreatedAt => _createdAt;
    public DateTimeOffset ExpiresAt => _expiresAt;
    public DateTimeOffset? UsedAt => _usedAt;

    private EmailConfirmationToken() { }

    private EmailConfirmationToken(Guid userId, TokenHash tokenHash, DateTimeOffset expiresAt)
    {
        _userId = userId;
        _tokenHash = tokenHash;
        _createdAt = DateTimeOffset.UtcNow;
        _expiresAt = expiresAt;
    }

    public static EmailConfirmationToken Create(Guid userId, TokenHash tokenHash, DateTimeOffset expiresAt)
    {
        if (userId == Guid.Empty)
            throw new DomainException(
                message: "UserId cannot be empty.",
                identifier: "USER_ID_INVALID");

        if (tokenHash is null)
            throw new DomainException(
                message: "Token hash cannot be null.",
                identifier: "TOKEN_HASH_NULL");

        if (expiresAt <= DateTimeOffset.UtcNow)
            throw new DomainException(
                message: "Expiration date must be in the future.",
                identifier: "TOKEN_EXPIRATION_INVALID");

        return new EmailConfirmationToken(userId, tokenHash, expiresAt);
    }

    public bool IsExpired(DateTimeOffset nowUtc)
        => nowUtc >= _expiresAt;

    public bool IsUsed()
        => _usedAt.HasValue;

    public void MarkAsUsed(DateTimeOffset usedAtUtc)
    {
        if (_usedAt.HasValue)
            throw new DomainException(
                message: "Token has already been used.",
                identifier: "TOKEN_ALREADY_USED");

        if (IsExpired(usedAtUtc))
            throw new DomainException(
                message: "Token has expired.",
                identifier: "TOKEN_EXPIRED");

        _usedAt = usedAtUtc;
    }
}