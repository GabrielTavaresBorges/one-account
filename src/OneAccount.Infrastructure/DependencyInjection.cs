using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneAccount.Application.Services.Security.Interfaces;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.Repositories.UsersRepository;
using OneAccount.Infrastructure.Data;
using OneAccount.Infrastructure.Data.Context;
using OneAccount.Infrastructure.Data.Repositories.UserRepository;
using OneAccount.Infrastructure.Identity.Services;

namespace OneAccount.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada. Verifique appsettings.json (Presentation.Server).");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, AspNetIdentityPasswordHasher>();

        return services;
    }
}

