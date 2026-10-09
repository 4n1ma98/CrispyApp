using CrispyApp.Application.Interfaces;
using CrispyApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CrispyApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<ICatalogService, CatalogService>();

        return services;
    }
}
