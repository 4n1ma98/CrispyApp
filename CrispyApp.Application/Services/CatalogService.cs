using System.Collections.Generic;
using System.Linq;
using CrispyApp.Application.DTOs;
using CrispyApp.Application.Interfaces;
using CrispyApp.Domain.Repositories;

namespace CrispyApp.Application.Services;

public class CatalogService : ICatalogService
{
    private readonly IOrderTypeRepository _orderTypeRepo;
    private readonly IPaymentMethodRepository _paymentMethodRepo;

    public CatalogService(IOrderTypeRepository orderTypeRepo, IPaymentMethodRepository paymentMethodRepo)
    {
        _orderTypeRepo = orderTypeRepo;
        _paymentMethodRepo = paymentMethodRepo;
    }

    public IEnumerable<OrderTypeDto> GetActiveOrderTypes()
    {
        return _orderTypeRepo.GetAll()
            .Where(x => x.IsActive)
            .Select(x => new OrderTypeDto { Id = x.Id, Name = x.Name });
    }

    public IEnumerable<PaymentMethodDto> GetActivePaymentMethods()
    {
        return _paymentMethodRepo.GetAll()
            .Where(x => x.IsActive)
            .Select(x => new PaymentMethodDto { Id = x.Id, Name = x.Name });
    }
}
