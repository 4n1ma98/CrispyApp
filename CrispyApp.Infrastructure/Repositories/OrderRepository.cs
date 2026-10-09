using System;
using System.Collections.Generic;
using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Repositories;
using CrispyApp.Infrastructure.Data;

namespace CrispyApp.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(LiteDbContext dbContext) : base(dbContext)
    {
        // Add index on Date for faster range queries
        _collection.EnsureIndex(x => x.Date);
    }

    public IEnumerable<Order> GetOrdersByDateRange(DateTime start, DateTime end)
    {
        return _collection.Find(x => x.Date >= start && x.Date <= end);
    }
}
