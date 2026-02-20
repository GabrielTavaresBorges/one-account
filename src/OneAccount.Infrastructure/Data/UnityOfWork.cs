using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Infrastructure.Data.Context;

namespace OneAccount.Infrastructure.Data;

public sealed class UnityOfWork : IUnityOfWork
{
    private readonly AppDbContext _context;

    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync() => await _context.SaveChangesAsync();
}
