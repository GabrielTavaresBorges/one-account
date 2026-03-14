namespace OneAccount.Application.Services.Security.Interfaces;

public interface IEmailConfirmationTokenService
{
    Task<GeneratedEmailConfirmationToken> GenerateTokenAsync();
}
