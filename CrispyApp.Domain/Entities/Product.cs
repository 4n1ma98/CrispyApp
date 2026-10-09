using System;
using System.Collections.Generic;
using CrispyApp.Domain.Enums;
using CrispyApp.Domain.ValueObjects;

namespace CrispyApp.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public List<ProductIngredient> Recipe { get; set; } = new();
}
