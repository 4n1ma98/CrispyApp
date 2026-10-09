using System.Collections.Generic;
using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Repositories;
using CrispyApp.Infrastructure.Data;

namespace CrispyApp.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(LiteDbContext dbContext) : base(dbContext)
    {
        // Add index on IsActive for faster querying
        _collection.EnsureIndex(x => x.IsActive);
    }

    public IEnumerable<Product> GetActiveProducts()
    {
        return _collection.Find(x => x.IsActive == true);
    }
}
