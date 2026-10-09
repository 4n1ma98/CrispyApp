using System;

namespace CrispyApp.Application.DTOs;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<ProductIngredientDto> Recipe { get; set; } = new();
}

public class ProductUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public List<ProductIngredientDto> Recipe { get; set; } = new();
}

public class InventoryItemCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal CurrentStock { get; set; }
    public decimal MinStockLevel { get; set; }
}

public class InventoryItemUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal MinStockLevel { get; set; }
}

public class InventoryItemAddStockDto
{
    public Guid Id { get; set; }
    public decimal QuantityToAdd { get; set; }
}
