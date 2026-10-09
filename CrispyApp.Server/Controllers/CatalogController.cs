using System;
using Microsoft.AspNetCore.Mvc;
using CrispyApp.Application.Interfaces;

namespace CrispyApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("order-types")]
    public IActionResult GetOrderTypes()
    {
        return Ok(_catalogService.GetActiveOrderTypes());
    }

    [HttpGet("payment-methods")]
    public IActionResult GetPaymentMethods()
    {
        return Ok(_catalogService.GetActivePaymentMethods());
    }
}
