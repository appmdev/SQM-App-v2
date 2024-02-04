using App.Query.Domain.Entities;

namespace App.Query.Api.Queries
{
    public interface IQueryHandler
    {
        Task<List<MapEntity>> HandleAsync(FindAllMapsQuery query);
        Task<List<MapEntity>> HandleAsync(FindMapByIdQuery query);
    }
}
