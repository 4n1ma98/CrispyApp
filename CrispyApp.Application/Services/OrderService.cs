using System;
using System.Linq;
using CrispyApp.Application.DTOs;
using CrispyApp.Application.Interfaces;
using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Enums;
using CrispyApp.Domain.Repositories;
using CrispyApp.Domain.ValueObjects;

namespace CrispyApp.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInventoryItemRepository _inventoryRepository;
    private readonly IOrderTypeRepository _orderTypeRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IInventoryItemRepository inventoryRepository,
        IOrderTypeRepository orderTypeRepository,
        IPaymentMethodRepository paymentMethodRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _inventoryRepository = inventoryRepository;
        _orderTypeRepository = orderTypeRepository;
        _paymentMethodRepository = paymentMethodRepository;
    }

    public OrderDto CreateOrder(OrderCreateRequestDto request)
    {
        // Get Catalog Names
        var orderType = _orderTypeRepository.GetById(request.OrderTypeId);
        var paymentMethod = _paymentMethodRepository.GetById(request.PaymentMethodId);
        
        if (orderType == null) throw new Exception("Invalid Order Type.");
        if (paymentMethod == null) throw new Exception("Invalid Payment Method.");

        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = new Random().Next(1000, 9999),
            Date = DateTime.UtcNow,
            OrderTypeId = orderType.Id,
            OrderTypeName = orderType.Name,
            PaymentMethodId = paymentMethod.Id,
            PaymentMethodName = paymentMethod.Name,
            Status = OrderStatus.Completed,
            CustomerInfo = request.CustomerInfo != null ? new CustomerInfo 
            { 
                Name = request.CustomerInfo.Name, 
                Phone = request.CustomerInfo.Phone, 
                Address = request.CustomerInfo.Address 
            } : null
        };

        foreach (var reqItem in request.Items)
        {
            var product = _productRepository.GetById(reqItem.ProductId);
            if (product == null || !product.IsActive)
                throw new Exception($"Product {reqItem.ProductId} not found or inactive.");

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = reqItem.Quantity,
                UnitPrice = product.Price,
                Notes = reqItem.Notes
            });

            foreach (var ingredient in product.Recipe)
            {
                var inventoryItem = _inventoryRepository.GetById(ingredient.InventoryItemId);
                if (inventoryItem != null)
                {
                    decimal totalDeduction = ingredient.QuantityToDeduct * reqItem.Quantity;
                    inventoryItem.CurrentStock -= totalDeduction;
                    _inventoryRepository.Update(inventoryItem);
                }
            }
        }

        _orderRepository.Add(order);

        return MapToDto(order);
    }

    public OrderDto GetOrderById(Guid id)
    {
        var order = _orderRepository.GetById(id);
        if (order == null) throw new Exception("Order not found");
        return MapToDto(order);
    }

    public IEnumerable<OrderDto> GetOrdersByDate(DateTime date)
    {
        return _orderRepository.GetAll()
            .Where(o => o.Date.Date == date.Date)
            .OrderByDescending(o => o.Date)
            .Select(MapToDto);
    }

    private OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            Date = order.Date,
            OrderTypeId = order.OrderTypeId,
            OrderTypeName = order.OrderTypeName,
            PaymentMethodId = order.PaymentMethodId,
            PaymentMethodName = order.PaymentMethodName,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                SubTotal = i.SubTotal,
                Notes = i.Notes
            }).ToList()
        };
    }
}
