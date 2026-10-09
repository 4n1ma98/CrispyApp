using CrispyApp.Domain.Entities;
using System.Collections.Generic;

namespace CrispyApp.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetActiveProducts();
}
