using OneAccount.Domain.Entities.User;

namespace OneAccount.Domain.Repositories.UsersRepository;

public interface IUserRepository : IRepository<User>
{
    Task CreateUserAsync(User user, CancellationToken cancellationToken);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
