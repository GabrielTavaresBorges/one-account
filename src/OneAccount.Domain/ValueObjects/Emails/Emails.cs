using System.Text.RegularExpressions;
using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Domain.ValueObjects.Emails;

public sealed partial record Email
{
    public string EmailAddress { get; private init; }
    private Email() { }

    private Email(string value)
    {
        EmailAddress = value;
    }

    public static Result<Email> Create(string email)
    {
        var validatedEmail = ValidateEmail(email);

        if (validatedEmail.IsFailure)
        {
            return Result<Email>.Failure(validatedEmail.Error);
        }

        return Result<Email>.Success(new Email(validatedEmail.Value));
    }

    private static Result<string> ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<string>.Failure(new Error(
                Identifier: "EMAIL_EMPTY",
                Message: "Email is required. " +
                         "Please provide an address in the format 'example@domain.com'."));
        }

        email = email.Trim();

        // limite comum (prático)
        const int MaxLength = 254;
        if (email.Length > MaxLength)
        {
            return Result<string>.Failure(new Error(
                Identifier: "EMAIL_TOO_LONG",
                Message: "Email is too long. " +
                         $"Current length: {email.Length} characters." +
                         $"Maximum allowed length: {MaxLength} characters."));
        }

        // Regex simples e eficiente (não tenta cobrir toda RFC)
        if (!EmailRegex().IsMatch(email))
        {
            return Result<string>.Failure(new Error(
                Identifier: "EMAIL_INVALID_FORMAT",
                Message: "Email format is invalid. " +
                        $"Expected format: 'example@domain.com'." +
                        $"Received value: '{email}'."));
        }

        return Result<string>.Success(email);
    }

    // Compilada e gerada em build (melhor performance e sem custo em runtime)
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();
}
