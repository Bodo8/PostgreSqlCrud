using ChatService.Domain.Models;
using ChatService.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async (AppDbContext db) =>
                await db.Orders.ToListAsync());

            app.MapGet("/orders/{id}", async (int id, AppDbContext db) =>
            {
                Order? order = await db.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

                if (order is null) return Results.NotFound();

                return Results.Ok(order);
            });

            app.MapPost("/orders", async (Order order, AppDbContext db) =>
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return Results.Created($"/users/{order.Id}", order);
            });

            app.MapPut("/orders/{id}", async (int id, Order inputOrder, AppDbContext db) =>
            {
                var order = await db.Orders.FindAsync(id);
                if (order is null) return Results.NotFound();

                order.Amount = inputOrder.Amount;
                order.UpdDate = DateTime.UtcNow;
                order.Status = inputOrder.Status;
                order.IsPaid = inputOrder.IsPaid;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
