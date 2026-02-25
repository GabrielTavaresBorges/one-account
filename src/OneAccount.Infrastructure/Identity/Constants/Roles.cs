namespace OneAccount.Infrastructure.Identity.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";

    public static readonly IReadOnlyCollection<string> All = new[] { Admin, User };
}
