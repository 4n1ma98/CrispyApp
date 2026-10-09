using System;
using System.Collections.Generic;
using System.Linq;
using CrispyApp.Application.DTOs;
using CrispyApp.Application.Interfaces;
using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Enums;
using CrispyApp.Domain.Repositories;

namespace CrispyApp.Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryItemRepository _inventoryRepository;

    public InventoryService(IInventoryItemRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public IEnumerable<InventoryItemDto> GetAllItems()
    {
        return _inventoryRepository.GetAll().Select(MapToDto);
    }

    public InventoryItemDto CreateItem(InventoryItemCreateDto request)
    {
        var count = _inventoryRepository.GetAll().Count() + 1;
        var item = new InventoryItem
        {
            Id = Guid.NewGuid(),
            Code = $"INS-{count:D3}",
            Name = request.Name,
            UnitOfMeasure = Enum.TryParse<UnitOfMeasure>(request.UnitOfMeasure, out var uom) ? uom : UnitOfMeasure.Unit,
            CurrentStock = request.CurrentStock,
            MinStockLevel = request.MinStockLevel
        };
        _inventoryRepository.Add(item);
        return MapToDto(item);
    }

    public InventoryItemDto UpdateItem(InventoryItemUpdateDto request)
    {
        var item = _inventoryRepository.GetById(request.Id);
        if (item == null) throw new Exception("Inventory item not found");

        item.Name = request.Name;
        item.UnitOfMeasure = Enum.TryParse<UnitOfMeasure>(request.UnitOfMeasure, out var uom) ? uom : item.UnitOfMeasure;
        item.MinStockLevel = request.MinStockLevel;

        _inventoryRepository.Update(item);
        return MapToDto(item);
    }

    public void DeleteItem(Guid id)
    {
        var item = _inventoryRepository.GetById(id);
        if (item != null)
        {
            _inventoryRepository.Delete(id);
        }
    }

    public InventoryItemDto AddStock(Guid id, decimal quantityToAdd)
    {
        var item = _inventoryRepository.GetById(id);
        if (item == null) throw new Exception("Inventory item not found");

        item.CurrentStock += quantityToAdd;
        _inventoryRepository.Update(item);
        return MapToDto(item);
    }

    private InventoryItemDto MapToDto(InventoryItem item)
    {
        return new InventoryItemDto
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name,
            UnitOfMeasure = item.UnitOfMeasure,
            CurrentStock = item.CurrentStock,
            MinStockLevel = item.MinStockLevel
        };
    }
}
