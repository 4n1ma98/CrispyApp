using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Repositories;
using CrispyApp.Infrastructure.Data;

namespace CrispyApp.Infrastructure.Repositories;

public class PaymentMethodRepository : Repository<PaymentMethodEntity>, IPaymentMethodRepository
{
    public PaymentMethodRepository(LiteDbContext dbContext) : base(dbContext)
    {
    }
}
