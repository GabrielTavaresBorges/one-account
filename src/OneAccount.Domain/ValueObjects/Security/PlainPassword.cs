using OneAccount.Domain.Abstraction.Records;
using System.Text.RegularExpressions;

namespace OneAccount.Domain.ValueObjects.Security;

public sealed record PlainPassword
{
    public string Password { get; }

    private PlainPassword(string password) => Password = password;

    private static readonly Regex UppercaseRegex = new("[A-Z]", RegexOptions.Compiled);
    private static readonly Regex LowercaseRegex = new("[a-z]", RegexOptions.Compiled);
    private static readonly Regex DigitRegex = new(@"\d", RegexOptions.Compiled);
    private static readonly Regex SpecialRegex = new("[^A-Za-z0-9]", RegexOptions.Compiled);

    public static Result<PlainPassword> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Result<PlainPassword>.Failure(new Error(
                Identifier: "PASSWORD_EMPTY",
                Message: "Password cannot be empty."));

        password = password.Trim();

        if (password.Length < 8)
            return Result<PlainPassword>.Failure(new Error(
                Identifier: "PASSWORD_TOO_SHORT",
                Message: "Password must be at least 8 chars."));

        if (!UppercaseRegex.IsMatch(password))
            return Result<PlainPassword>.Failure(new Error(
                Identifier: "PASSWORD_MISSING_UPPERCASE",
                Message: "Password must contain at least one uppercase letter."));

        if (!LowercaseRegex.IsMatch(password))
            return Result<PlainPassword>.Failure(new Error(
                Identifier: "PASSWORD_MISSING_LOWERCASE",
                Message: "Password must contain at least one lowercase letter."));

        if (!DigitRegex.IsMatch(password))
            return Result<PlainPassword>.Failure(new Error(
                Identifier: "PASSWORD_MISSING_DIGIT",
                Message: "Password must contain at least one number."));

        if (!SpecialRegex.IsMatch(password))
            return Result<PlainPassword>.Failure(new Error(
                Identifier: "PASSWORD_MISSING_SPECIAL",
                Message: "Password must contain at least one special character."));

        const int MaxLength = 256;
        if (password.Length > MaxLength)
            return Result<PlainPassword>.Failure(new Error(
                Identifier: "PASSWORD_TOO_LONG",
                Message: $"Password is too long. Maximum allowed length is {MaxLength} characters."));

        return Result<PlainPassword>.Success(new PlainPassword(password));
    }
}