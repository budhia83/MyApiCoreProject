//namespace ApiGateway.DTOs
//{
//    public class PaymentDto
//    {
//        public Guid PaymentId { get; set; }
//        public Guid OrderId { get; set; }
//        public string Status { get; set; }
//    }

//}
namespace ApiGateway.DTOs
{
    public class PaymentDto
    {
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; } // e.g., Pending, Completed, Failed, Refunded
        public decimal Amount { get; set; }
    }

    public class PaymentRequest
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } // e.g., CreditCard, PayPal, UPI
    }
}
