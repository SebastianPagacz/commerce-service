using Microsoft.Extensions.DependencyInjection;

namespace ProductService.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection RegisterMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

        return services;
    }
}