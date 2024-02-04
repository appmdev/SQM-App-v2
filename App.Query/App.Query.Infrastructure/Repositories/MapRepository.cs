using App.Query.Domain.Entities;
using App.Query.Domain.Repositories;
using App.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace App.Query.Infrastructure.Repositories
{
    public class MapRepository : IMapRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public MapRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(MapEntity map)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Maps.Add(map);
            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid mapId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var map = await GetByIdAsync(mapId);
            if (map == null) return;

            context.Maps.Remove(map);
            _ = await context.SaveChangesAsync();
        }

        public async Task<MapEntity> GetByIdAsync(Guid mapId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Maps
                    .Include(m => m.Pointclouds)
                    .FirstOrDefaultAsync(x => x.MapId == mapId);
        }

        public async Task<List<MapEntity>> ListAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Maps.AsNoTracking()
                    .Include(m => m.Pointclouds).AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<MapEntity>> ListByAuthorAsync(string author)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Maps.AsNoTracking()
                    .Include(m => m.Pointclouds).AsNoTracking()
                    .Where(x => x.Author.Contains(author))
                    .ToListAsync();
        }

        public async Task<List<MapEntity>> ListWithPointcloudsAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Maps.AsNoTracking()
                    .Include(m => m.Pointclouds).AsNoTracking()
                    .Where(x => x.Pointclouds != null && x.Pointclouds.Any())
                    .ToListAsync();
        }
    }
}