using System;
using CrispyApp.Domain.Enums;

namespace CrispyApp.Domain.Entities;

public class InventoryMovement : BaseEntity
{
    public Guid InventoryItemId { get; set; }
    public MovementType Type { get; set; }
    public decimal Quantity { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Reason { get; set; } = string.Empty;
    public Guid? ReferenceId { get; set; } // e.g. OrderId if triggered by a sale
}
