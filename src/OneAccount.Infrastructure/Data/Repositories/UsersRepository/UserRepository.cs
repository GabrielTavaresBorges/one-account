using Microsoft.EntityFrameworkCore;
using OneAccount.Domain.Entities.User;
using OneAccount.Domain.Repositories.UsersRepository;
using OneAccount.Infrastructure.Data.Context;

namespace OneAccount.Infrastructure.Data.Repositories.UserRepository;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.User.AddAsync(user, cancellationToken);
    }

    public Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        _context.User.Update(user);
        return Task.CompletedTask;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}
