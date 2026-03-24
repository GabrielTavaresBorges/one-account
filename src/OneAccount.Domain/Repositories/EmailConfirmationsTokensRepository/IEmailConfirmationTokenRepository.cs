using OneAccount.Domain.Entities.EmailConfirmationTokens;

namespace OneAccount.Domain.Repositories.EmailConfirmationsTokensRepository;

public interface IEmailConfirmationTokenRepository : IRepository<EmailConfirmationToken>
{
    Task CreateEmailConfirmationTokenAsync(EmailConfirmationToken emailConfirmationToken, CancellationToken cancellationToken);
}
