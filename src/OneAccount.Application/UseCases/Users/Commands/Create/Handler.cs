using MediatR;
using Microsoft.Extensions.Logging;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.Abstraction.Records;
using OneAccount.Domain.Entities.Users;
using OneAccount.Domain.Repositories.UsersRepository;
using OneAccount.Domain.ValueObjects.Documents;
using OneAccount.Domain.ValueObjects.Emails;
using OneAccount.Domain.ValueObjects.Names;

namespace OneAccount.Application.UseCases.Users.Commands.Create;

public sealed class Handler : IRequestHandler<Command, Result<Response>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnityOfWork _unitOfWork;
    private readonly ILogger<Handler> _logger;

    public Handler(IUsersRepository usersRepository, IUnityOfWork unitOfWork, ILogger<Handler> logger)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Response>> Handle(Command command, CancellationToken cancellationToken)
    {
        try
        {
            var userNameResult = UserName.Create(command.UserName);
            if (userNameResult.IsFailure)
            {
                return Result<Response>.Failure(userNameResult.Error);
            }

            var emailResult = Email.Create(command.EmailAddress);
            if (emailResult.IsFailure)
            {
                return Result<Response>.Failure(emailResult.Error);
            }

            var cpfResult = Cpf.Create(command.CpfNumber);
            if (cpfResult.IsFailure)
            {
                return Result<Response>.Failure(cpfResult.Error);
            }

            var user = User.Create(
                userName: userNameResult.Value,
                emailAddress: emailResult.Value,
                cpfNumber: cpfResult.Value);

            await _usersRepository.CreateUserAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync();

            return Result<Response>.Success(
                new Response(
                    id: user.Id,
                    userName: user.UserName.Name,
                    message: "User created successfully!")
                );
        }
        catch (DomainException ex)
        {
            _logger.LogError(ex, "Domain Error creating user.");
            return Result<Response>.Failure(
                new Error(ex.Identifier ?? "DOMAIN_ERROR", ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating user.");
            return Result<Response>.Failure(
                new Error("UNEXPECTED_ERROR", "An unexpected error occurred."));
        }
    }
}
