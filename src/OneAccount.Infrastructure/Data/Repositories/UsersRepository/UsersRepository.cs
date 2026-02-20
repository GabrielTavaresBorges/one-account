using OneAccount.Domain.Entities.Users;
using OneAccount.Domain.Repositories.UsersRepository;
using OneAccount.Infrastructure.Data.Context;

namespace OneAccount.Infrastructure.Data.Repositories.UserRepository;

public sealed class UsersRepository(AppDbContext context) : IUsersRepository
{
    public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }
}
