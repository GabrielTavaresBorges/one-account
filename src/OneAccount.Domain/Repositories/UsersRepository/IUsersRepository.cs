using OneAccount.Domain.Entities.Users;

namespace OneAccount.Domain.Repositories.UsersRepository;

public interface IUsersRepository : IRepository<User>
{
    Task CreateUserAsync(User user, CancellationToken cancellationToken);
}
