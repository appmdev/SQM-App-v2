using App.Query.Domain.Entities;

namespace App.Query.Domain.Repositories
{
    public interface IMapRepository
    {
        Task CreateAsync(MapEntity map);
        Task DeleteAsync(Guid mapId);
        Task<MapEntity> GetByIdAsync(Guid mapId);
        Task<List<MapEntity>> ListAllAsync();
        Task<List<MapEntity>> ListByAuthorAsync(string author);
        Task<List<MapEntity>> ListWithPointcloudsAsync();
    }
}