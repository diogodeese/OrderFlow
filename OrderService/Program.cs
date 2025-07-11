using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Controllers;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core Context
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlite("Data Source=orders.db"));

builder.Services.AddScoped<IOrderService, OrderService.Services.OrderService>();

// Add other services (CORS, Swagger, etc.)
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Endpoints
app.MapGet("/", () => "OrderService Running");

app.MapGroup("/orders").MapOrderRoutes();

app.Urls.Add("http://0.0.0.0:5064");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    db.Database.EnsureCreated();
}

app.Run();