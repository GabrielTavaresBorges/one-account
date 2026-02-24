using MediatR;
using OneAccount.Domain.Abstraction.Records;
using OneAccount.Domain.ValueObjects.Dates;

namespace OneAccount.Application.UseCases.Users.Commands.Update;

public sealed record Command(
    Guid Id,
    string? Email,
    string? Password,
    string? UserName,
    BirthDate? BirthDate
    ) : IRequest<Result<Response>>
{

}