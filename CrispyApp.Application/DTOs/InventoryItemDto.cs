using System;
using CrispyApp.Domain.Enums;

namespace CrispyApp.Application.DTOs;

public class InventoryItemDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public UnitOfMeasure UnitOfMeasure { get; set; }
    public decimal CurrentStock { get; set; }
    public decimal MinStockLevel { get; set; }
}
