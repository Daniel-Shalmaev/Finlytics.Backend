using MongoDB.Driver;
using Finlytics.Application.Interfaces;
using Finlytics.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Finlytics.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Finlytics.Application.Interfaces.Repositories;
using Finlytics.Infrastructure.Repositories.MongoRepositories;

namespace Finlytics.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // JWT generation and validation logic
        services.AddSingleton<JwtService>();

        // MongoDB connection and base access
        services.AddSingleton<MongoDbService>();

        // Resolve IMongoDatabase for repositories
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var mongoDbService = sp.GetRequiredService<MongoDbService>();
            return mongoDbService.GetDatabase();
        });

        // Core business services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IFinanceService, FinanceService>();

        // Generic Mongo repository registration
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

        return services;
    }
}


