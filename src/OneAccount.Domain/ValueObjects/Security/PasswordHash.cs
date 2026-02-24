using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Domain.ValueObjects.Security;

public sealed record PasswordHash
{
    public string Password { get; }

    private PasswordHash(string password)
    {
        Password = password;
    }

    public static Result<PasswordHash> Create(string value)
    {
        var validatedPassword = Validate(value);

        if (validatedPassword.IsFailure)
            return Result<PasswordHash>.Failure(validatedPassword.Error);

        return Result<PasswordHash>.Success(new PasswordHash(value));
    }

    private static Result<string> Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<string>.Failure(new Error(
                Identifier: "PASSWORD_HASH_EMPTY",
                Message: "Password hash cannot be null or empty."));
        }

        value = value.Trim();

        const int MaxLength = 1024;
        if (value.Length > MaxLength)
        {
            return Result<string>.Failure(new Error(
                Identifier: "PASSWORD_HASH_TOO_LONG",
                Message: "Password hash is too long.\n" +
                         $"Current length: {value.Length} characters.\n" +
                         $"Maximum allowed length: {MaxLength} characters."
            ));
        }

        return Result<string>.Success(value);
    }
}