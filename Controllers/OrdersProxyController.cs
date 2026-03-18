using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("gateway/orders")]
    [ApiController]
    public class OrdersProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public OrdersProxyController(IHttpClientFactory factory) =>
            _httpClient = factory.CreateClient("OrderService");

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] object order)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", order);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

    }
}
