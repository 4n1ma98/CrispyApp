using System;

namespace CrispyApp.Domain.Entities;

public class PaymentMethodEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
