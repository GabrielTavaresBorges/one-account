using FluentValidation;
using OneAccount.Domain.Enumerators;

namespace OneAccount.Application.UseCases.Users.Commands.Create;

public sealed class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(254)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(256);            

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("BirthDate cannot be in the future.");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .NotEqual(Gender.Unknown);

        RuleFor(x => x.PhoneType)
            .IsInEnum()
            .NotEqual(PhoneType.Unknown);

        RuleFor(x => x.CallingCode)
            .NotEmpty()
            .Matches(@"^\+\d{1,3}$")
            .WithMessage("CallingCode must be in format +<digits> (e.g., +55, +1).");

        RuleFor(x => x.RegionCode)
            .NotEmpty()
            .Length(2)
            .Matches("^[A-Za-z]{2}$")
            .WithMessage("RegionCode must be a valid ISO2 code (e.g., BR, US).");

        RuleFor(x => x.AreaCode)
            .Matches(@"^\d+$")
            .When(x => !string.IsNullOrWhiteSpace(x.AreaCode))
            .WithMessage("AreaCode must contain digits only.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d+$")
            .WithMessage("PhoneNumber must contain digits only.");

        RuleFor(x => x.E164)
            .NotEmpty()
            .Matches(@"^\+\d+$")
            .WithMessage("E164 must be in format +<digits>.");
                
        RuleFor(x => x)
            .Must(HasConsistentE164)
            .WithMessage("E164 is inconsistent with CallingCode/AreaCode/PhoneNumber.");

    }

    private static bool HasConsistentE164(Command cmd)
    {           
        var calling = (cmd.CallingCode ?? "").Trim();
        var area = (cmd.AreaCode ?? "").Trim();
        var number = (cmd.PhoneNumber ?? "").Trim();
        var e164 = (cmd.E164 ?? "").Trim();

        if (string.IsNullOrWhiteSpace(calling) ||
            string.IsNullOrWhiteSpace(number) ||
            string.IsNullOrWhiteSpace(e164))
            return true;

        var expected = $"{calling}{area}{number}".Replace(" ", "");
        return string.Equals(e164, expected, StringComparison.Ordinal);
    }
}
