using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Controllers;
using OrderService.Services;
using Microsoft.OpenApi.Models;
using OrderService.Messaging;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("ORDER_SERVICE_PORT") ?? "5064";

// Register EF Core Context
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlite("Data Source=orders.db"));

builder.Services.AddScoped<IOrderService, OrderService.Services.OrderService>();
builder.Services.AddSingleton<OrderCreatedPublisher>();
builder.Services.AddHostedService<RabbitMqHostedService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Service API",
        Version = "v1",
        Description = "API for managing orders in the OrderFlow system"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoints
app.MapGet("/", () => "OrderService running!");

app.MapGroup("/orders").MapOrderRoutes();

app.Urls.Add($"http://0.0.0.0:{port}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    db.Database.Migrate();
}

app.Run();