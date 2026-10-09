using System;
using Microsoft.AspNetCore.Mvc;
using CrispyApp.Application.DTOs;
using CrispyApp.Application.Interfaces;

namespace CrispyApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] OrderCreateRequestDto request)
    {
        var order = _orderService.CreateOrder(request);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetOrderById(Guid id)
    {
        return Ok(_orderService.GetOrderById(id));
    }

    [HttpGet]
    public IActionResult GetOrders([FromQuery] DateTime? date)
    {
        var targetDate = date ?? DateTime.UtcNow.Date;
        return Ok(_orderService.GetOrdersByDate(targetDate));
    }
}
