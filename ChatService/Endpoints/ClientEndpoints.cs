using ChatService.Domain.Dto;
using ChatService.Domain.Models;
using ChatService.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Endpoints
{
    public static class ClientEndpoints
    {
        public static void MapClientEndpoints(this IEndpointRouteBuilder app) 
        {
            app.MapGet("/clients", async (AppDbContext db) => {

                var clients = await db.Clients
                .Include(c => c.Orders)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    City = c.City,
                    Orders = c.Orders.Select(o => new OrderDto
                    {
                        Id = o.Id,
                        Amount = o.Amount,
                        CreatedDate = o.CreatedDate.Date,
                        UpdDate = o.UpdDate.Date
                    }).ToList()
                })
                .ToListAsync();

                return Results.Ok(clients);
            });

            app.MapGet("/clients/{id}", async (int id, AppDbContext db) =>
            {
                ClientDto? client = await db.Clients
                .Include(c => c.Orders)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    City = c.City,
                    Orders = c.Orders.Select(o => new OrderDto
                    {
                        Id = o.Id,
                        Amount = o.Amount,
                        CreatedDate = o.CreatedDate.Date,
                        UpdDate = o.UpdDate.Date
                    }).ToList()
                })
                .FirstOrDefaultAsync(o => o.Id == id);

                if (client is null)
                    return Results.NotFound();

                return Results.Ok(client);
            });

            app.MapPost("/clients", async (Client client, AppDbContext db) =>
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();

                return Results.Created($"/users/{client.Id}", client);
            });

            app.MapPut("/clients/{id}", async (int id, Client inputClient, AppDbContext db) =>
            {
                var client = await db.Clients.FindAsync(id);

                if (client is null) return Results.NotFound();

                client.FirstName = inputClient.FirstName;
                client.LastName = inputClient.LastName;
                client.Email = inputClient.Email;
                client.City = inputClient.City;
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
