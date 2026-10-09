using System;
using System.Collections.Generic;
using CrispyApp.Domain.Enums;

namespace CrispyApp.Application.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public List<ProductIngredientDto> Recipe { get; set; } = new();
}

public class ProductIngredientDto
{
    public Guid InventoryItemId { get; set; }
    public decimal QuantityToDeduct { get; set; }
}
