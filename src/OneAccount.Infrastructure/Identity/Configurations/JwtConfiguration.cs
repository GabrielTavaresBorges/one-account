using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OneAccount.Infrastructure.Identity.Configurations;

public static class JwtConfiguration
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO:
        // Implementar configuração de autenticação JWT.
        //
        // Passos previstos:
        //
        // 1. Ler configurações do appsettings:
        //    - Jwt:Key
        //    - Jwt:Issuer
        //    - Jwt:Audience
        //    - Jwt:ExpirationInMinutes (se aplicável)
        //
        // 2. Validar se a chave (Jwt:Key) está configurada corretamente.
        //    - Garantir que não é nula ou vazia.
        //    - Futuramente considerar armazenamento seguro (ex: variáveis de ambiente).
        //
        // 3. Configurar AddAuthentication:
        //    - Definir DefaultAuthenticateScheme como JwtBearer.
        //    - Definir DefaultChallengeScheme como JwtBearer.
        //
        // 4. Configurar AddJwtBearer:
        //    - Habilitar validação de:
        //        * Issuer
        //        * Audience
        //        * Lifetime
        //        * IssuerSigningKey
        //    - Definir parâmetros de validação (TokenValidationParameters).
        //    - Configurar ClockSkew conforme necessidade.
        //
        // 5. Garantir consistência entre:
        //    - Claims geradas no TokenService.
        //    - Policies definidas em AuthorizationPolicies.
        //    - Configurações de validação aqui definidas.

        return services;
    }
}
