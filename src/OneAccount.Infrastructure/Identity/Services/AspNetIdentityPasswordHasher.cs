using Microsoft.AspNetCore.Identity;
using OneAccount.Application.Services.Security.Interfaces;

namespace OneAccount.Infrastructure.Identity.Services;

public sealed class AspNetIdentityPasswordHasher : IPasswordHasher
{
    // O PasswordHasher do ASP.NET Identity já implementa:
    // - salt por senha
    // - formato versionado
    // - comparação segura
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        // O parâmetro "user" aqui não é necessário no nosso caso
        return _hasher.HashPassword(null!, password);
    }

    public bool Verify(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
            return false;

        if (string.IsNullOrWhiteSpace(providedPassword))
            return false;

        var result = _hasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);

        // SuccessRehashNeeded = senha correta, mas o hash deveria ser atualizado
        // (por exemplo, após upgrade de parâmetros/versão). Você pode tratar isso
        // futuramente no login para re-hash automático.
        return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
