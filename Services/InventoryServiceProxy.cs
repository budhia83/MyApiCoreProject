using ApiGateway.DTOs.ApiGateway.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace ApiGateway.Services
{
    

   
        public class InventoryServiceProxy
        {
            private readonly HttpClient _httpClient;

            public InventoryServiceProxy(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // Get inventory details for a product
            public async Task<InventoryDto?> GetInventoryAsync(string productId)
            {
                var response = await _httpClient.GetAsync($"/api/inventory/{productId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<InventoryDto>();
            }

            // Update stock levels
            public async Task<bool> UpdateStockAsync(string productId, int quantity)
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/inventory/{productId}", new { Quantity = quantity });
                return response.IsSuccessStatusCode;
            }

            // Reserve stock for an order
            public async Task<bool> ReserveStockAsync(string productId, int quantity)
            {
                var response = await _httpClient.PostAsJsonAsync("/api/inventory/reserve", new { ProductId = productId, Quantity = quantity });
                return response.IsSuccessStatusCode;
            }
        }

}
