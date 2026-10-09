using System.Collections.Generic;
using CrispyApp.Application.DTOs;

namespace CrispyApp.Application.Interfaces;

public interface ICatalogService
{
    IEnumerable<OrderTypeDto> GetActiveOrderTypes();
    IEnumerable<PaymentMethodDto> GetActivePaymentMethods();
}
