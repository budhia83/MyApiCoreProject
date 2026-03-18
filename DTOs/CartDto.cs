namespace ApiGateway.DTOs
{
    public class CartDto
    {
        public string CustomerId { get; set; }
        public List<string> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }

}
