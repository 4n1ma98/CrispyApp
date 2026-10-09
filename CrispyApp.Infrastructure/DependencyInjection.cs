using CrispyApp.Domain.Repositories;
using CrispyApp.Infrastructure.Data;
using CrispyApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrispyApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Register LiteDbContext as Singleton (thread-safe in LiteDB version 5+)
        services.AddSingleton(new LiteDbContext(connectionString));

        // Register Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        
        // Catalogs
        services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
