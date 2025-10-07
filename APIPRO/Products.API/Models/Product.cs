namespace Products.API.Models
{
    public class GlobalExceptionHandlingMiddleware
    {
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public byte[]? Photo { get; set; }
    }
}
