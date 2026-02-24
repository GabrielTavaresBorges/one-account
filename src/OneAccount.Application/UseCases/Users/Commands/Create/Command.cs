using MediatR;
using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Application.UseCases.Users.Commands.Create;

public sealed record Command(
    string Email,
    string Password,
    string UserName,    
    string CpfNumber,
    DateOnly BirthDate) : IRequest<Result<Response>>
{
}
