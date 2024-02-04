using App.Query.Domain.Entities;

namespace App.Query.Domain.Repositories
{
    public interface IPointcloudRepository
    {
        Task CreateAsync(PointcloudEntity pointcloud);
        Task<PointcloudEntity> GetByIdAsync(Guid pointcoudId);
    }
}