using MediatR;
using OneAccount.Domain.Abstraction.Records;
using OneAccount.Domain.Enumerators;

namespace OneAccount.Application.UseCases.Users.Commands.Create;

public sealed record Command(
    string Email,
    string Password,
    string UserName,
    DateOnly BirthDate,
    Gender Gender,
    string CallingCode,
    string RegionCode,
    string? AreaCode,
    PhoneType PhoneType,
    string PhoneNumber,
    string E164) : IRequest<Result<Response>>
{
}
