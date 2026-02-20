using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Domain.ValueObjects.Names;

public sealed record UserName
{
    public string Name { get; }

    private UserName(string name)
    {
        Name = name;
    }

    public static Result<UserName> Create(string name)
    {
        var validatedName = ValidateName(name);

        if (validatedName.IsFailure)
            return Result<UserName>.Failure(validatedName.Error);

        return Result<UserName>.Success(new UserName(validatedName.Value));
    }

    private static Result<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<string>.Failure(new Error(
             Identifier: "USER_NAME_EMPTY",
             Message: "Name cannot be null or empty."));
        }

        name = name.Trim();

        if (name.Length < 2 || name.Length > 100)
        {
            return Result<string>.Failure(new Error(
               Identifier: "USER_NAME_INVALID_LENGTH",
               Message: "Name must be between 2 and 100 characters."));
        }

        return Result<string>.Success(name);
    }
}
