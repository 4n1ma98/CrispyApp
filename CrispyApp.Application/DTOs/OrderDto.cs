using System;
using System.Collections.Generic;
using CrispyApp.Domain.Enums;

namespace CrispyApp.Application.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime Date { get; set; }
    
    public Guid OrderTypeId { get; set; }
    public string OrderTypeName { get; set; } = string.Empty;
    
    public Guid PaymentMethodId { get; set; }
    public string PaymentMethodName { get; set; } = string.Empty;
    
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }
    public string? Notes { get; set; }
}
