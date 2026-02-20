using MediatR;
using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Application.UseCases.Users.Commands.Update;

public sealed record Command(
    Guid Id,
    string? UserName,
    string? EmailAddress) : IRequest<Result<Response>>
{

}