namespace ProductService.Models;

public class OrderCreatedEvent
{
    public int Id { get; set; }
    public string Customer { get; set; } = string.Empty;
    public double Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public int TotalQuantity => Items.Sum(i => i.Quantity);
}

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
}