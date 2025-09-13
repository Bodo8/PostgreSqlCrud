using ChatService.Domain.Models;
using ChatService.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users", async (AppDbContext db) =>
                await db.Users.ToListAsync());

            app.MapGet("/users/{id}", async (int id, AppDbContext db) =>
                await db.Users.FindAsync(id) is User user ? Results.Ok(user) : Results.NotFound());

            app.MapPost("/users", async (User user, AppDbContext db) =>
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return Results.Created($"/users/{user.Id}", user);
            });

            app.MapPut("/users/{id}", async (int id, User inputUser, AppDbContext db) =>
            {
                var user = await db.Users.FindAsync(id);
                if (user is null) return Results.NotFound();

                user.Name = inputUser.Name;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/users/{id}", async (int id, AppDbContext db) =>
            {
                var user = await db.Users.FindAsync(id);
                if (user is null) return Results.NotFound();

                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
