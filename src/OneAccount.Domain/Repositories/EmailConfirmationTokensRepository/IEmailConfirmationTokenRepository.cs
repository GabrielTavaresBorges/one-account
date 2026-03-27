using OneAccount.Domain.Entities.EmailConfirmationTokens;

namespace OneAccount.Domain.Repositories.EmailConfirmationTokensRepository;

public interface IEmailConfirmationTokenRepository : IRepository<EmailConfirmationToken>
{
    Task CreateEmailConfirmationTokenAsync(EmailConfirmationToken emailConfirmationToken, CancellationToken cancellationToken);
}
