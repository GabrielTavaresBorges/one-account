using MediatR;
using Microsoft.Extensions.Logging;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Abstraction.Records;
using OneAccount.Domain.Repositories.UsersRepository;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.ValueObjects.Emails;
using OneAccount.Domain.ValueObjects.Names;

namespace OneAccount.Application.UseCases.Users.Commands.Update;

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
            // 1) Carrega o usuário atual
            // Ajuste o nome do método conforme seu repositório (ex.: GetByIdAsync / FindByIdAsync)
            var user = await _usersRepository.GetByIdAsync(command.Id, cancellationToken);
            if (user is null)
            {
                return Result<Response>.Failure(
                    new Error("USER_NOT_FOUND", "User not found."));
            }

            var updatedFields = new List<string>();

            // 2) Atualiza somente o que veio no comando (update parcial)
            if (command.UserName is not null)
            {
                // Se quiser evitar marcar como atualizado quando for igual ao atual:
                if (!string.Equals(user.UserName.Name, command.UserName, StringComparison.Ordinal))
                {
                    var userNameResult = UserName.Create(command.UserName);
                    if (userNameResult.IsFailure)
                        return Result<Response>.Failure(userNameResult.Error);

                    user.ChangeUserName(userNameResult.Value);
                    updatedFields.Add("UserName");
                }
            }

            if (command.EmailAddress is not null)
            {
                // Se quiser evitar marcar como atualizado quando for igual ao atual:
                if (!string.Equals(user.Email.EmailAddress, command.EmailAddress, StringComparison.OrdinalIgnoreCase))
                {
                    var emailResult = Email.Create(command.EmailAddress);
                    if (emailResult.IsFailure)
                        return Result<Response>.Failure(emailResult.Error);

                    user.ChangeEmail(emailResult.Value);
                    updatedFields.Add("EmailAddress");
                }
            }

            // 3) Se nada mudou (ex.: mandou o mesmo valor), retorne uma resposta “no-op”
            if (updatedFields.Count == 0)
            {
                return Result<Response>.Success(
                    new Response(
                        id: user.Id,
                        updatedFields: Array.Empty<string>(),
                        message: "No changes to apply."
                    )
                );
            }

            // 4) Persistência
            // Se você usa EF Core e o user já está rastreado, pode nem precisar chamar Update.
            // Ajuste conforme seu repositório:
            await _usersRepository.UpdateUserAsync(user, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            // 5) Mensagem amigável
            var message = updatedFields.Count switch
            {
                1 when updatedFields[0] == "UserName" => "User name updated successfully!",
                1 when updatedFields[0] == "EmailAddress" => "Email updated successfully!",
                _ => "User name and email updated successfully!"
            };

            return Result<Response>.Success(
                new Response(
                    id: user.Id,
                    updatedFields: updatedFields,
                    message: message
                )
            );
        }
        catch (DomainException ex)
        {
            _logger.LogError(ex, "Domain error updating user.");
            return Result<Response>.Failure(
                new Error(ex.Identifier ?? "DOMAIN_ERROR", ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error updating user.");
            return Result<Response>.Failure(
                new Error("UNEXPECTED_ERROR", "An unexpected error occurred."));
        }
    }
}