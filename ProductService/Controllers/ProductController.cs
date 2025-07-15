using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Services;

namespace ProductService.Controllers
{
    public static class ProductController
    {
        public static RouteGroupBuilder MapProductRoutes(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] Product product, IProductService service) =>
            {
                var created = await service.CreateAsync(product);
                return Results.Created($"/products/{created.Id}", created);
            });

            group.MapGet("/", async (IProductService service) =>
            {
                var products = await service.GetAllAsync();
                return Results.Ok(products);
            });

            group.MapGet("/{id:int}", async (int id, IProductService service) =>
            {
                var product = await service.GetByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });

            group.MapPut("/{id:int}", async (int id, [FromBody] Product updatedProduct, IProductService service) =>
            {
                var success = await service.UpdateAsync(id, updatedProduct);
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id:int}", async (int id, IProductService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}