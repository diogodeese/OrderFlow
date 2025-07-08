var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var orders = new List<object>
{
    new { Id = 1, Customer = "Catarina", Total = 150.50, Status = "Pending" },
    new { Id = 2, Customer = "Diogo", Total = 80.00, Status = "Shipped" }
};

app.MapGet("/", () => "Order Service is running 🚀");

app.MapGet("/orders", () => orders);

app.Run();
