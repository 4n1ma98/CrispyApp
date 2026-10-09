using System;
using System.Collections.Generic;
using CrispyApp.Application.DTOs;

namespace CrispyApp.Application.Interfaces;

public interface IOrderService
{
    OrderDto CreateOrder(OrderCreateRequestDto request);
    OrderDto GetOrderById(Guid id);
    IEnumerable<OrderDto> GetOrdersByDate(DateTime date);
}
