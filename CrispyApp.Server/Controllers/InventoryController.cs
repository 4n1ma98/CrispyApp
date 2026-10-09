using System;
using Microsoft.AspNetCore.Mvc;
using CrispyApp.Application.DTOs;
using CrispyApp.Application.Interfaces;

namespace CrispyApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public IActionResult GetAllInventoryItems()
    {
        return Ok(_inventoryService.GetAllItems());
    }

    [HttpPost]
    public IActionResult CreateItem([FromBody] InventoryItemCreateDto request)
    {
        return Ok(_inventoryService.CreateItem(request));
    }

    [HttpPut]
    public IActionResult UpdateItem([FromBody] InventoryItemUpdateDto request)
    {
        return Ok(_inventoryService.UpdateItem(request));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteItem(Guid id)
    {
        _inventoryService.DeleteItem(id);
        return NoContent();
    }

    [HttpPost("{id:guid}/add-stock")]
    public IActionResult AddStock(Guid id, [FromBody] decimal quantity)
    {
        return Ok(_inventoryService.AddStock(id, quantity));
    }
}
