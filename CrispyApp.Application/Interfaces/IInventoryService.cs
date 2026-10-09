using System;
using System.Collections.Generic;
using CrispyApp.Application.DTOs;

namespace CrispyApp.Application.Interfaces;

public interface IInventoryService
{
    IEnumerable<InventoryItemDto> GetAllItems();
    InventoryItemDto CreateItem(InventoryItemCreateDto request);
    InventoryItemDto UpdateItem(InventoryItemUpdateDto request);
    void DeleteItem(Guid id);
    InventoryItemDto AddStock(Guid id, decimal quantityToAdd);
}
