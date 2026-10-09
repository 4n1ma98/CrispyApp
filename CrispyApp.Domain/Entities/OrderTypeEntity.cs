using System;

namespace CrispyApp.Domain.Entities;

public class OrderTypeEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
