using OneAccount.Domain.Abstraction.Exceptions;

namespace OneAccount.Domain.ValueObjects.Security;

public sealed record PasswordHash
{
    public string Value { get; }

    private PasswordHash(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Cria um PasswordHash a partir de um hash já gerado (ex.: Argon2/BCrypt/PBKDF2).
    /// O domínio não gera hash, apenas valida e armazena.
    /// </summary>
    public static PasswordHash FromHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Password hash cannot be null or empty.", "PASSWORD_HASH_EMPTY");

        // Limite defensivo (hashes costumam ser bem menores; isso evita payloads absurdos)
        if (value.Length > 1024)
            throw new DomainException("Password hash is too long.", "PASSWORD_HASH_TOO_LONG");

        return new PasswordHash(value);
    }
}