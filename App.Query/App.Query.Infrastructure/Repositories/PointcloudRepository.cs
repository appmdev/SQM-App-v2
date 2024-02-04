
using App.Query.Domain.Entities;
using App.Query.Domain.Repositories;
using App.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace App.Query.Infrastructure.Repositories
{
    public class PointcloudRepository : IPointcloudRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public PointcloudRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(PointcloudEntity pointcloud)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Pointclouds.Add(pointcloud);

            _ = await context.SaveChangesAsync();
        }

        public async Task<PointcloudEntity> GetByIdAsync(Guid pointcoudId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Pointclouds.FirstOrDefaultAsync(x => x.PointcloudId == pointcoudId);
        }
    }
}
