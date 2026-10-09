using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Repositories;
using CrispyApp.Infrastructure.Data;

namespace CrispyApp.Infrastructure.Repositories;

public class OrderTypeRepository : Repository<OrderTypeEntity>, IOrderTypeRepository
{
    public OrderTypeRepository(LiteDbContext dbContext) : base(dbContext)
    {
    }
}
