using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OneAccount.Application.Services.Security.Interfaces;
using OneAccount.Infrastructure.Identity.Models;

namespace OneAccount.Infrastructure.Identity.Services;

public sealed class TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration) : ITokenService
{
    // TODO:
    // Implementar geração de JWT seguindo as configurações definidas em JwtConfiguration.
    // Passos previstos:
    //
    // 1. Recuperar o usuário via UserManager a partir do userId.
    // 2. Validar se o usuário existe e está ativo (se aplicável).
    // 3. Construir lista de claims:
    //    - NameIdentifier (Id do Identity)
    //    - domain_user_id (vínculo com usuário do domínio)
    //    - Roles do usuário
    //    - Outras claims necessárias (ex: is_active, se for usar policy).
    // 4. Criar SigningCredentials usando Jwt:Key.
    // 5. Definir issuer, audience e tempo de expiração via IConfiguration.
    // 6. Gerar JwtSecurityToken.
    // 7. Retornar token serializado (string).
    //
    // Observação importante:
    // Garantir consistência entre:
    // - Claims emitidas aqui
    // - Policies configuradas em AuthorizationPolicies
    // - Validações definidas em JwtConfiguration

    Task<string> ITokenService.GenerateTokenAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
