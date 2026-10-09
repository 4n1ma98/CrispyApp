using System;

namespace CrispyApp.Domain.ValueObjects;

public class ProductIngredient
{
    public Guid InventoryItemId { get; set; }
    public decimal QuantityToDeduct { get; set; }
}
