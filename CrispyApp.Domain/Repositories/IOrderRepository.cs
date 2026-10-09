using System;
using System.Collections.Generic;
using CrispyApp.Domain.Entities;

namespace CrispyApp.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    IEnumerable<Order> GetOrdersByDateRange(DateTime start, DateTime end);
}
