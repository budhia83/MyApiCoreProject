using ApiGateway.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiGateway.Services
{
    public class ShippingServiceProxy
    {
        private readonly HttpClient _httpClient;

        public ShippingServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Create a shipping request for an order
        public async Task<ShippingDto?> CreateShipmentAsync(ShippingRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/shipping", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ShippingDto>();
        }

        // Get shipping status by shipment ID
        public async Task<ShippingDto?> GetShipmentStatusAsync(string shipmentId)
        {
            var response = await _httpClient.GetAsync($"/api/shipping/{shipmentId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ShippingDto>();
        }

        // Update shipment (e.g., address change, reschedule)
        public async Task<bool> UpdateShipmentAsync(string shipmentId, ShippingUpdate update)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/shipping/{shipmentId}", update);
            return response.IsSuccessStatusCode;
        }
    }
}
