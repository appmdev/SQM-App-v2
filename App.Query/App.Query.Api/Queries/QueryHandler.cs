using App.Query.Domain.Entities;
using App.Query.Domain.Repositories;

namespace App.Query.Api.Queries
{
    public class QueryHandler : IQueryHandler
    {
        private readonly IMapRepository _mapRepository;
        public QueryHandler(IMapRepository repository)
        {
            _mapRepository = repository;
        }

        public async Task<List<MapEntity>> HandleAsync(FindAllMapsQuery query)
        {
            return await _mapRepository.ListAllAsync();
        }

        public async Task<List<MapEntity>> HandleAsync(FindMapByIdQuery query)
        {
            var map = await _mapRepository.GetByIdAsync(query.Id);
            return new List<MapEntity> { map };
        }
    }
}
