using ApiGateway.DTOs;
using ApiGateway.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartServiceProxy _cartService;

        public CartController(CartServiceProxy cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CartDto>> GetCart(string customerId)
        {
            var cart = await _cartService.GetCartAsync(customerId);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
    }
}
