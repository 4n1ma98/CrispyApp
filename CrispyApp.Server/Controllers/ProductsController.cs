using Microsoft.AspNetCore.Mvc;
using CrispyApp.Application.DTOs;
using CrispyApp.Application.Interfaces;

namespace CrispyApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetActiveProducts()
    {
        return Ok(_productService.GetActiveProducts());
    }

    [HttpGet("all")]
    public IActionResult GetAllProducts()
    {
        return Ok(_productService.GetAllProducts());
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] ProductCreateDto request)
    {
        return Ok(_productService.CreateProduct(request));
    }

    [HttpPut]
    public IActionResult UpdateProduct([FromBody] ProductUpdateDto request)
    {
        return Ok(_productService.UpdateProduct(request));
    }

    [HttpPut("{id:guid}/toggle")]
    public IActionResult ToggleStatus(Guid id)
    {
        return Ok(_productService.ToggleProductStatus(id));
    }
}
