using Finlytics.Application.Interfaces;
using Finlytics.Infrastructure.Persistence;
using Finlytics.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Finlytics.Infrastructure.Services;

namespace Finlytics.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<MongoDbService>();
        services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        services.AddScoped<IFinanceService, FinanceService>();

        return services;
    }
}

