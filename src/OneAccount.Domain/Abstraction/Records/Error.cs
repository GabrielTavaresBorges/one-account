namespace OneAccount.Domain.Abstraction.Records;

public sealed record Error(string Identifier, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public bool IsNone =>
        string.IsNullOrWhiteSpace(Identifier) && string.IsNullOrWhiteSpace(Message);
}