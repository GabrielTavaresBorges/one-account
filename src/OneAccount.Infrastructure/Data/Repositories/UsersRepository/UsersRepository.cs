using Microsoft.EntityFrameworkCore;
using OneAccount.Domain.Entities.Users;
using OneAccount.Domain.Repositories.UsersRepository;
using OneAccount.Infrastructure.Data.Context;

namespace OneAccount.Infrastructure.Data.Repositories.UserRepository;

public sealed class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}
