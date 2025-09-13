using ChatService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Infrastructure.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Order> Orders => Set<Order>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // nazwa tabeli
                entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

                // nazwy kolumn
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.GetColumnBaseName()));
                }
            }
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var chars = new List<char>();
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsUpper(c))
                {
                    if (i > 0) chars.Add('_');
                    chars.Add(char.ToLower(c));
                }
                else
                {
                    chars.Add(c);
                }
            }
            return new string(chars.ToArray());
        }
    }
}
