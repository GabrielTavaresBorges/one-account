namespace OneAccount.Application.Services.Security.Interfaces;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(Guid userId);
}
