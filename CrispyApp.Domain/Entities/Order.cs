using System;
using System.Collections.Generic;
using System.Linq;
using CrispyApp.Domain.Enums;
using CrispyApp.Domain.ValueObjects;

namespace CrispyApp.Domain.Entities;

public class Order : BaseEntity
{
    public int OrderNumber { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public CustomerInfo? CustomerInfo { get; set; }
    
    // Referenced from Catalog
    public Guid OrderTypeId { get; set; }
    public string OrderTypeName { get; set; } = string.Empty;
    
    // Referenced from Catalog
    public Guid PaymentMethodId { get; set; }
    public string PaymentMethodName { get; set; } = string.Empty;
    
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    public List<OrderItem> Items { get; set; } = new();

    public decimal TotalAmount => Items.Sum(i => i.SubTotal);
}
