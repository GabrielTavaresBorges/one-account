using FluentValidation;
using OneAccount.Domain.Enumerators;

namespace OneAccount.Application.UseCases.Users.Commands.Create;

public sealed class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(150)
            .EmailAddress();

        RuleFor(x => x.CpfNumber)
            .NotEmpty()
            .Length(11);

        RuleFor(x => x.Gender)
            .IsInEnum()
            .NotEqual(Gender.Unknown);
    }
}