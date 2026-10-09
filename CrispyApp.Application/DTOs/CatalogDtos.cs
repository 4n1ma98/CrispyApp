using System;

namespace CrispyApp.Application.DTOs;

public class OrderTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class PaymentMethodDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
