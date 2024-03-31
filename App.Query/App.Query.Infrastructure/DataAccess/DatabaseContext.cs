using App.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Query.Infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MapEntity> Maps { get; set; }
        public DbSet<StateEntity> States { get; set; }
    }
}