using MediatR;
using OneAccount.Domain.Abstraction.Records;
using OneAccount.Domain.Enumerators;

namespace OneAccount.Application.UseCases.Users.Commands.Create;

public sealed record Command(
    string Email,
    string Password,
    string UserName,    
    string CpfNumber,
    DateOnly BirthDate, 
    Gender Gender) : IRequest<Result<Response>>
{
}
