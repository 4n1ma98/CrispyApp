using System;
using System.Collections.Generic;
using CrispyApp.Application.DTOs;

namespace CrispyApp.Application.Interfaces;

public interface IProductService
{
    IEnumerable<ProductDto> GetActiveProducts();
    IEnumerable<ProductDto> GetAllProducts();
    ProductDto CreateProduct(ProductCreateDto request);
    ProductDto UpdateProduct(ProductUpdateDto request);
    ProductDto ToggleProductStatus(Guid id);
}
