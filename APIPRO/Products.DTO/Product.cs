namespace Products.DTO;
public sealed class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public byte[]? Photo { get; set; }
}
