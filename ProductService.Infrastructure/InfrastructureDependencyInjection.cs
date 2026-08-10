using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;
using ProductService.Infrastructure.Repository;

namespace ProductService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Product>, ProductRepository>();
        services.AddScoped<IRepository<Category>, CategoryRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(connectionString));

        return services;
    }
}