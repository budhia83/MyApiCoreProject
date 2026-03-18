using Microsoft.AspNetCore.Mvc;
using ApiGateway.Services;
using ApiGateway.DTOs;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly ShippingServiceProxy _shippingService;

        public ShippingController(ShippingServiceProxy shippingService)
        {
            _shippingService = shippingService;
        }

        [HttpPost]
        public async Task<ActionResult<ShippingDto>> CreateShipment(ShippingRequest request)
        {
            var shipment = await _shippingService.CreateShipmentAsync(request);
            return CreatedAtAction(nameof(GetShipmentStatus), new { shipmentId = shipment.ShipmentId }, shipment);
        }

        [HttpGet("{shipmentId}")]
        public async Task<ActionResult<ShippingDto>> GetShipmentStatus(string shipmentId)
        {
            var shipment = await _shippingService.GetShipmentStatusAsync(shipmentId);
            if (shipment == null) return NotFound();
            return Ok(shipment);
        }

        [HttpPut("{shipmentId}")]
        public async Task<IActionResult> UpdateShipment(string shipmentId, ShippingUpdate update)
        {
            var success = await _shippingService.UpdateShipmentAsync(shipmentId, update);
            if (!success) return BadRequest();
            return NoContent();
        }
    }
}
