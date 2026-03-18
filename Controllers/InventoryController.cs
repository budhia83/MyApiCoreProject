using ApiGateway.DTOs;
using ApiGateway.DTOs.ApiGateway.DTOs;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{

        [ApiController]
        [Route("api/[controller]")]
        public class InventoryController : ControllerBase
        {
            private readonly InventoryServiceProxy _inventoryService;

            public InventoryController(InventoryServiceProxy inventoryService)
            {
                _inventoryService = inventoryService;
            }

            [HttpGet("{productId}")]
            public async Task<ActionResult<InventoryDto>> GetInventory(string productId)
            {
                var inventory = await _inventoryService.GetInventoryAsync(productId);
                if (inventory == null) return NotFound();
                return Ok(inventory);
            }

            [HttpPut("{productId}")]
            public async Task<IActionResult> UpdateStock(string productId, [FromBody] int quantity)
            {
                var success = await _inventoryService.UpdateStockAsync(productId, quantity);
                if (!success) return BadRequest();
                return NoContent();
            }

            [HttpPost("reserve")]
            public async Task<IActionResult> ReserveStock([FromBody] ReserveStockRequest request)
            {
                var success = await _inventoryService.ReserveStockAsync(request.ProductId, request.Quantity);
                if (!success) return BadRequest();
                return Ok();
            }
        }

        public class ReserveStockRequest
        {
            public string ProductId { get; set; }
            public int Quantity { get; set; }
        }

}
