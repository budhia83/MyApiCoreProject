using ApiGateway.DTOs;

namespace ApiGateway.Services
{
    public class CartServiceProxy
    {
        private readonly HttpClient _httpClient;

        public CartServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartDto?> GetCartAsync(string customerId)
        {
            //var response = await _httpClient.GetAsync($"/api/cart/{customerId}");
            //response.EnsureSuccessStatusCode();
            //return await response.Content.ReadFromJsonAsync<CartDto>();
            try
            {
                var response = await _httpClient.GetAsync($"/api/cart/{customerId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CartDto>();
            }
            catch (Exception ex)
            {
                // Log the error
                //_logger.LogError(ex, "Failed to fetch cart for {CustomerId}", customerId);

                // Fallback: return empty cart
                return new CartDto { CustomerId = customerId, Items = new List<string> { ex.Message.ToString()} };
            }

        }
    }

}
