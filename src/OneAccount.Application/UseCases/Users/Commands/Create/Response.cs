namespace OneAccount.Application.UseCases.Users.Commands.Create;

public sealed record Response
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Message { get; init; }

    public Response(Guid id, string userName, string message)
    {
        Id = id;
        UserName = userName;
        Message = message;
    }
}
