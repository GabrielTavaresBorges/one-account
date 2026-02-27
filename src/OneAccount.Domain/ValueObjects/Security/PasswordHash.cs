using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Domain.ValueObjects.Security;

public sealed record PasswordHash
{
    public string Password { get; }

    private PasswordHash(string password)
    {
        Password = password;
    }

    public static Result<PasswordHash> Create(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            return Result<PasswordHash>.Failure(new Error(
                "PASSWORD_HASH_EMPTY",
                "Password hash cannot be null or empty."));

        passwordHash = passwordHash.Trim();

        const int MaxLength = 1024;
        if (passwordHash.Length > MaxLength)
            return Result<PasswordHash>.Failure(new Error(
                "PASSWORD_HASH_TOO_LONG",
                $"Password hash is too long. Current length: {passwordHash.Length}. Max: {MaxLength}."));

        return Result<PasswordHash>.Success(new PasswordHash(passwordHash));
    }
}