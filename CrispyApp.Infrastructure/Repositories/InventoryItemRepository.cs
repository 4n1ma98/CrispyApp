using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Repositories;
using CrispyApp.Infrastructure.Data;

namespace CrispyApp.Infrastructure.Repositories;

public class InventoryItemRepository : Repository<InventoryItem>, IInventoryItemRepository
{
    public InventoryItemRepository(LiteDbContext dbContext) : base(dbContext)
    {
        _collection.EnsureIndex(x => x.Code, unique: true);
    }
}
