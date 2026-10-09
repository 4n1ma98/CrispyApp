using System;
using CrispyApp.Domain.Enums;

namespace CrispyApp.Domain.Entities;

public class InventoryItem : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public UnitOfMeasure UnitOfMeasure { get; set; }
    public decimal CurrentStock { get; set; }
    public decimal MinStockLevel { get; set; }
}
