using OneAccount.Domain.Entities.EmailConfirmations;
using OneAccount.Domain.Repositories.EmailConfirmationsTokensRepository;
using OneAccount.Infrastructure.Data.Context;

namespace OneAccount.Infrastructure.Data.Repositories.EmailConfirmationsTokensRepository;

public sealed class EmailConfirmationTokenRepository : IEmailConfirmationTokenRepository
{
    private readonly AppDbContext _context;

    public EmailConfirmationTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateEmailConfirmationTokenAsync(EmailConfirmationToken emailConfirmationToken, CancellationToken cancellationToken)
    {
        await _context.EmailConfirmationTokens.AddAsync(emailConfirmationToken, cancellationToken);
    }
}
