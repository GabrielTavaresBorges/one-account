namespace OneAccount.Application.Services.Interfaces;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(Guid userId);
}
