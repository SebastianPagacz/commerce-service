using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Services.QueryServices;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Exceptions;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;
using ProductService.Infrastructure.QueryServices;
using ProductService.Infrastructure.Repository;

namespace ProductService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Product>, ProductRepository>();
        services.AddScoped<IRepository<Category>, CategoryRepository>();
        
        services.AddScoped<IProductQueryService, ProductQueryService>();
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>     
            options.UseNpgsql(connectionString));

        return services;
    }
}