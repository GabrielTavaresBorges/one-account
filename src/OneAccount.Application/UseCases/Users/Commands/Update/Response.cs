namespace OneAccount.Application.UseCases.Users.Commands.Update;

public sealed record Response
{
    public Guid Id { get; init; }
    public IReadOnlyCollection<string> UpdatedFields { get; init; }
    public string Message { get; init; }

    public Response(Guid id, IReadOnlyCollection<string> updatedFields, string message)
    {
        Id = id;
        UpdatedFields = updatedFields;
        Message = message;
    }
}