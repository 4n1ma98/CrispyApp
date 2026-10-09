using System;
using System.Collections.Generic;
using System.Linq;
using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Enums;
using CrispyApp.Domain.Repositories;
using CrispyApp.Domain.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CrispyApp.Server;

public static class DataSeeder
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var inventoryRepo = scope.ServiceProvider.GetRequiredService<IInventoryItemRepository>();
        var productRepo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
        var orderTypeRepo = scope.ServiceProvider.GetRequiredService<IOrderTypeRepository>();
        var paymentMethodRepo = scope.ServiceProvider.GetRequiredService<IPaymentMethodRepository>();
        var userRepo = scope.ServiceProvider.GetRequiredService<CrispyApp.Infrastructure.Repositories.IUserRepository>();

        // Seed default admin
        if (!userRepo.AnyUsers())
        {
            userRepo.Add(new CrispyApp.Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                PasswordHash = userRepo.HashPassword("admin123"),
                Role = "Admin",
                IsActive = true
            });
        }

        // Check if data already exists
        if (orderTypeRepo.GetAll().Any())
            return; // Already seeded

        // Seed Catalogs (Colombianized)
        orderTypeRepo.Add(new OrderTypeEntity { Id = Guid.NewGuid(), Name = "Consumo en Local" });
        orderTypeRepo.Add(new OrderTypeEntity { Id = Guid.NewGuid(), Name = "Para Llevar" });
        orderTypeRepo.Add(new OrderTypeEntity { Id = Guid.NewGuid(), Name = "A Domicilio" });

        paymentMethodRepo.Add(new PaymentMethodEntity { Id = Guid.NewGuid(), Name = "Efectivo" });
        paymentMethodRepo.Add(new PaymentMethodEntity { Id = Guid.NewGuid(), Name = "Tarjeta" });
        paymentMethodRepo.Add(new PaymentMethodEntity { Id = Guid.NewGuid(), Name = "Nequi" });
        paymentMethodRepo.Add(new PaymentMethodEntity { Id = Guid.NewGuid(), Name = "Daviplata" });
        paymentMethodRepo.Add(new PaymentMethodEntity { Id = Guid.NewGuid(), Name = "Breb" });

        // Seed Inventory
        var polloId = Guid.NewGuid();
        var papaId = Guid.NewGuid();
        var gaseosaInsumoId = Guid.NewGuid();

        inventoryRepo.Add(new InventoryItem
        {
            Id = polloId,
            Code = "INS-001",
            Name = "Pollo Fresco",
            UnitOfMeasure = UnitOfMeasure.Kg,
            CurrentStock = 50.0m, // 50 Kg
            MinStockLevel = 10.0m
        });

        inventoryRepo.Add(new InventoryItem
        {
            Id = papaId,
            Code = "INS-002",
            Name = "Papas",
            UnitOfMeasure = UnitOfMeasure.Kg,
            CurrentStock = 100.0m, // 100 Kg
            MinStockLevel = 20.0m
        });

        inventoryRepo.Add(new InventoryItem
        {
            Id = gaseosaInsumoId,
            Code = "INS-003",
            Name = "Postobón 1.5L",
            UnitOfMeasure = UnitOfMeasure.Unit,
            CurrentStock = 100.0m, // 100 Units
            MinStockLevel = 24.0m
        });

        // Seed Products (Prices in COP)
        productRepo.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "1/4 de Pollo a la Brasa",
            Category = ProductCategory.Pollos,
            Price = 18000m,
            IsActive = true,
            Recipe = new List<ProductIngredient>
            {
                new ProductIngredient { InventoryItemId = polloId, QuantityToDeduct = 0.25m },
                new ProductIngredient { InventoryItemId = papaId, QuantityToDeduct = 0.35m }
            }
        });
        
        productRepo.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "1/2 Pollo a la Brasa",
            Category = ProductCategory.Pollos,
            Price = 35000m,
            IsActive = true,
            Recipe = new List<ProductIngredient>
            {
                new ProductIngredient { InventoryItemId = polloId, QuantityToDeduct = 0.5m },
                new ProductIngredient { InventoryItemId = papaId, QuantityToDeduct = 0.5m }
            }
        });

        productRepo.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Postobón 1.5L",
            Category = ProductCategory.Bebidas,
            Price = 8000m,
            IsActive = true,
            Recipe = new List<ProductIngredient>
            {
                new ProductIngredient { InventoryItemId = gaseosaInsumoId, QuantityToDeduct = 1.0m }
            }
        });
        
        productRepo.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Combo Familiar (Pollo + Papas + Gaseosa)",
            Category = ProductCategory.Promociones,
            Price = 75000m,
            IsActive = true,
            Recipe = new List<ProductIngredient>
            {
                new ProductIngredient { InventoryItemId = polloId, QuantityToDeduct = 1.0m },
                new ProductIngredient { InventoryItemId = papaId, QuantityToDeduct = 1.0m },
                new ProductIngredient { InventoryItemId = gaseosaInsumoId, QuantityToDeduct = 1.0m }
            }
        });
    }
}
