namespace ApiGateway.DTOs
{
    public class ShippingDto
    {
        public string ShipmentId { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; } // Pending, InTransit, Delivered, Cancelled
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
    }

    public class ShippingRequest
    {
        public string OrderId { get; set; }
        public string Address { get; set; }
        public string Carrier { get; set; }
    }

    public class ShippingUpdate
    {
        public string Address { get; set; }
        public string DeliveryDate { get; set; }
    }
}
