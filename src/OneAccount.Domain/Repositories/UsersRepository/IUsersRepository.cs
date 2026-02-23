using OneAccount.Domain.Entities.Users;

namespace OneAccount.Domain.Repositories.UsersRepository;

public interface IUsersRepository : IRepository<Users>
{
    Task CreateUserAsync(Users user, CancellationToken cancellationToken);
    Task UpdateUserAsync(Users user, CancellationToken cancellationToken);

    Task<Users?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
