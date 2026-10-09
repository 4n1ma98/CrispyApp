using System;
using System.Collections.Generic;
using System.Linq;
using CrispyApp.Application.DTOs;
using CrispyApp.Application.Interfaces;
using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Repositories;
using CrispyApp.Domain.ValueObjects;

namespace CrispyApp.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<ProductDto> GetActiveProducts()
    {
        return _productRepository.GetAll()
            .Where(p => p.IsActive)
            .Select(MapToDto);
    }

    public IEnumerable<ProductDto> GetAllProducts()
    {
        return _productRepository.GetAll().Select(MapToDto);
    }

    public ProductDto CreateProduct(ProductCreateDto request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Category = Enum.TryParse<CrispyApp.Domain.Enums.ProductCategory>(request.Category, out var cat) ? cat : CrispyApp.Domain.Enums.ProductCategory.Pollos,
            Price = request.Price,
            IsActive = true,
            Recipe = request.Recipe.Select(r => new ProductIngredient 
            { 
                InventoryItemId = r.InventoryItemId, 
                QuantityToDeduct = r.QuantityToDeduct 
            }).ToList()
        };
        _productRepository.Add(product);
        return MapToDto(product);
    }

    public ProductDto UpdateProduct(ProductUpdateDto request)
    {
        var product = _productRepository.GetById(request.Id);
        if (product == null) throw new Exception("Product not found");

        product.Name = request.Name;
        product.Category = Enum.TryParse<CrispyApp.Domain.Enums.ProductCategory>(request.Category, out var cat) ? cat : product.Category;
        product.Price = request.Price;
        product.IsActive = request.IsActive;
        product.Recipe = request.Recipe.Select(r => new ProductIngredient 
        { 
            InventoryItemId = r.InventoryItemId, 
            QuantityToDeduct = r.QuantityToDeduct 
        }).ToList();

        _productRepository.Update(product);
        return MapToDto(product);
    }

    public ProductDto ToggleProductStatus(Guid id)
    {
        var product = _productRepository.GetById(id);
        if (product == null) throw new Exception("Product not found");

        product.IsActive = !product.IsActive;
        _productRepository.Update(product);
        return MapToDto(product);
    }

    private ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            IsActive = product.IsActive,
            Recipe = product.Recipe.Select(r => new ProductIngredientDto
            {
                InventoryItemId = r.InventoryItemId,
                QuantityToDeduct = r.QuantityToDeduct
            }).ToList()
        };
    }
}
