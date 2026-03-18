namespace ApiGateway.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<string> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
