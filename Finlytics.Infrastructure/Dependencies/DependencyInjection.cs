using Finlytics.Application.Interfaces;
using Finlytics.Infrastructure.Persistence;
using Finlytics.Infrastructure.Repositories;
using Finlytics.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Finlytics.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<MongoDbService>();

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var mongoDbService = sp.GetRequiredService<MongoDbService>();
            return mongoDbService.GetDatabase();
        });

        services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        services.AddScoped<IFinanceService, FinanceService>();
        //services.AddScoped<ICompanyService, CompanyService>();


        return services;

    }
}

