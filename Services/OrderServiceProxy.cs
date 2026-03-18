using ApiGateway.DTOs;

namespace ApiGateway.Services
{
    public class OrderServiceProxy
    {
        private readonly HttpClient _httpClient;

        public OrderServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDto?> GetOrderAsync(Guid orderId)
        {
            var response = await _httpClient.GetAsync($"http://orderservice/api/orders/{orderId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderDto>();
        }

        public async Task<OrderDto?> CreateOrderAsync(OrderDto order)
        {
            var response = await _httpClient.PostAsJsonAsync("http://orderservice/api/orders", order);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderDto>();
        }
    }

}
