using System.Net.Http.Json;
using CrispyApp.Application.DTOs;

namespace CrispyApp.Client.Services;

public class PosApiService
{
    private readonly HttpClient _http;

    public PosApiService(HttpClient http)
    {
        _http = http;
    }

    // --- AUTH ---
    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        }
        return null;
    }

    public async Task<bool> VerifyPasswordAsync(string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/verify-password", new VerifyPasswordRequestDto { Password = password });
        return response.IsSuccessStatusCode;
    }

    // --- PRODUCTS ---
    public async Task<List<ProductDto>> GetActiveProductsAsync() => 
        await _http.GetFromJsonAsync<List<ProductDto>>("api/products") ?? new();

    public async Task<List<ProductDto>> GetAllProductsAsync() => 
        await _http.GetFromJsonAsync<List<ProductDto>>("api/products/all") ?? new();

    public async Task<ProductDto?> CreateProductAsync(ProductCreateDto request)
    {
        var response = await _http.PostAsJsonAsync("api/products", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductUpdateDto request)
    {
        var response = await _http.PutAsJsonAsync("api/products", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }

    public async Task<ProductDto?> ToggleProductStatusAsync(Guid id)
    {
        var response = await _http.PutAsync($"api/products/{id}/toggle", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }

    // --- CATALOGS ---
    public async Task<List<OrderTypeDto>> GetOrderTypesAsync() => 
        await _http.GetFromJsonAsync<List<OrderTypeDto>>("api/catalog/order-types") ?? new();

    public async Task<List<PaymentMethodDto>> GetPaymentMethodsAsync() => 
        await _http.GetFromJsonAsync<List<PaymentMethodDto>>("api/catalog/payment-methods") ?? new();

    // --- ORDERS ---
    public async Task<OrderDto?> CreateOrderAsync(OrderCreateRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("api/orders", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrderDto>();
    }

    public async Task<List<OrderDto>> GetOrdersByDateAsync(DateTime date) => 
        await _http.GetFromJsonAsync<List<OrderDto>>($"api/orders?date={date:yyyy-MM-dd}") ?? new();

    // --- INVENTORY ---
    public async Task<List<InventoryItemDto>> GetInventoryItemsAsync() => 
        await _http.GetFromJsonAsync<List<InventoryItemDto>>("api/inventory") ?? new();

    public async Task<InventoryItemDto?> CreateInventoryItemAsync(InventoryItemCreateDto request)
    {
        var response = await _http.PostAsJsonAsync("api/inventory", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InventoryItemDto>();
    }

    public async Task<InventoryItemDto?> UpdateInventoryItemAsync(InventoryItemUpdateDto request)
    {
        var response = await _http.PutAsJsonAsync("api/inventory", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InventoryItemDto>();
    }

    public async Task DeleteInventoryItemAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"api/inventory/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<InventoryItemDto?> AddStockAsync(Guid id, decimal quantity)
    {
        var response = await _http.PostAsJsonAsync($"api/inventory/{id}/add-stock", quantity);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InventoryItemDto>();
    }
}
