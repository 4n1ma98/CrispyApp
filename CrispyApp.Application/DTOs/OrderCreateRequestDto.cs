using System;
using System.Collections.Generic;

namespace CrispyApp.Application.DTOs;

public class OrderCreateRequestDto
{
    public Guid OrderTypeId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public CustomerInfoDto? CustomerInfo { get; set; }
    public List<OrderItemCreateDto> Items { get; set; } = new();
}

public class CustomerInfoDto
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class OrderItemCreateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Notes { get; set; }
}
