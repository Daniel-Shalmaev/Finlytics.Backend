using MongoDB.Driver;
using Finlytics.Application.Interfaces;
using Finlytics.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Finlytics.Infrastructure.Persistence;
using Finlytics.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Finlytics.Application.Interfaces.Repositories;

namespace Finlytics.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<JwtService>();
        services.AddSingleton<MongoDbService>();

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var mongoDbService = sp.GetRequiredService<MongoDbService>();
            return mongoDbService.GetDatabase();
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IFinanceService, FinanceService>();

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        return services;
    }
}

