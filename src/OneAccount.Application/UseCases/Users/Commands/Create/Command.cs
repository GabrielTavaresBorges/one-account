using MediatR;
using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Application.UseCases.Users.Commands.Create;

public sealed record Command(
    string UserName,
    string EmailAddress,
    string CpfNumber) : IRequest<Result<Response>>
{
}
