using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.Repositories.UsersRepository;
using OneAccount.Infrastructure.Data;
using OneAccount.Infrastructure.Data.Context;
using OneAccount.Infrastructure.Data.Repositories.UserRepository;

namespace OneAccount.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

