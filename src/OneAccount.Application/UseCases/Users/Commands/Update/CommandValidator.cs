using FluentValidation;

namespace OneAccount.Application.UseCases.Users.Commands.Update;

public sealed class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.UserName) || !string.IsNullOrWhiteSpace(x.EmailAddress))
            .WithMessage("Provide at least one field to update (UserName or EmailAddress).");

        When(x => x.UserName is not null, () =>
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);
        });

        When(x => x.EmailAddress is not null, () =>
        {
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .MaximumLength(150)
                .EmailAddress();
        });
    }
}