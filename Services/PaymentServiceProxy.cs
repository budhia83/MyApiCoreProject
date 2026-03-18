using ApiGateway.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace ApiGateway.Services
{

 
        public class PaymentServiceProxy
        {
            private readonly HttpClient _httpClient;

            public PaymentServiceProxy(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // Initiate a payment for an order
            public async Task<PaymentDto?> ProcessPaymentAsync(PaymentRequest request)
            {
                var response = await _httpClient.PostAsJsonAsync("/api/payments", request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PaymentDto>();
            }

            // Get payment status by ID
            public async Task<PaymentDto?> GetPaymentStatusAsync(string paymentId)
            {
                var response = await _httpClient.GetAsync($"/api/payments/{paymentId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PaymentDto>();
            }

            // Refund a payment
            public async Task<bool> RefundPaymentAsync(string paymentId)
            {
                var response = await _httpClient.PostAsync($"/api/payments/{paymentId}/refund", null);
                return response.IsSuccessStatusCode;
            }
        }
}
