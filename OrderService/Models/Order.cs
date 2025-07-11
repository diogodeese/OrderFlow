namespace OrderService.Models;

public class Order
{
    public int Id { get; set; }
    public string Customer { get; set; } = string.Empty;
    public double Total { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}