using Microsoft.AspNetCore.Mvc;
using ApiGateway.Services;
using ApiGateway.DTOs;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentServiceProxy _paymentService;

        public PaymentController(PaymentServiceProxy paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> ProcessPayment(PaymentRequest request)
        {
            var payment = await _paymentService.ProcessPaymentAsync(request);
            return CreatedAtAction(nameof(GetPaymentStatus), new { paymentId = payment.PaymentId }, payment);
        }

        [HttpGet("{paymentId}")]
        public async Task<ActionResult<PaymentDto>> GetPaymentStatus(string paymentId)
        {
            var payment = await _paymentService.GetPaymentStatusAsync(paymentId);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost("{paymentId}/refund")]
        public async Task<IActionResult> RefundPayment(string paymentId)
        {
            var success = await _paymentService.RefundPaymentAsync(paymentId);
            if (!success) return BadRequest();
            return Ok();
        }
    }
}
