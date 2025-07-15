using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers
{
    public static class OrderController
    {
        public static RouteGroupBuilder MapOrderRoutes(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] Order order, IOrderService service) =>
            {
                var created = await service.CreateAsync(order);
                return Results.Created($"/orders/{created.Id}", created);
            });
            
            group.MapGet("/", async (IOrderService service) =>
            {
                var orders = await service.GetAllAsync();
                return Results.Ok(orders);
            });

            group.MapGet("/{id:int}", async (int id, IOrderService service) =>
            {
                var order = await service.GetByIdAsync(id);
                return order is not null ? Results.Ok(order) : Results.NotFound();
            });

            group.MapPut("/{id:int}", async (int id, [FromBody] Order updatedOrder, IOrderService service) =>
            {
                var success = await service.UpdateAsync(id, updatedOrder);
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id:int}", async (int id, IOrderService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}