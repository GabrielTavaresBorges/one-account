using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Domain.ValueObjects.Security;

public sealed record TokenHash
{
    public string Token { get; }

    private TokenHash(string token)
    {
        Token = token;
    }

    public static Result<TokenHash> Create(string tokenHash)
    {
        if (string.IsNullOrWhiteSpace(tokenHash))
            return Result<TokenHash>.Failure(new Error(
                Identifier: "TOKEN_HASH_EMPTY",
                Message: "Token hash cannot be null or empty."));

        tokenHash = tokenHash.Trim();

        const int MaxLength = 1024;
        if (tokenHash.Length > MaxLength)
            return Result<TokenHash>.Failure(new Error(
                Identifier: "TOKEN_HASH_TOO_LONG",
                Message: $"Token hash is too long. Current length: {tokenHash.Length}. Max: {MaxLength}."));

        return Result<TokenHash>.Success(new TokenHash(tokenHash));
    }
}