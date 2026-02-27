namespace OneAccount.Application.Services.Security.Interfaces;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(Guid userId);
}
