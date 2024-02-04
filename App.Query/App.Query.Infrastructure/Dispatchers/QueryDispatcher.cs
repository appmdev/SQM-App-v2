using App.Query.Domain.Entities;
using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;

namespace App.Query.Infrastructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher<MapEntity>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<MapEntity>>>> _handlers = new();
        public void RegisterHandler<TQuery>(Func<TQuery, Task<List<MapEntity>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
            {
                throw new IndexOutOfRangeException("You cannot register the same query twice!");
            }

            _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
        }

        public async Task<List<MapEntity>> SendAsync(BaseQuery query)
        {
            if(_handlers.TryGetValue(query.GetType(), out Func<BaseQuery,Task<List<MapEntity>>> handler))
            {
                return await handler(query);
            }

            throw new ArgumentNullException(nameof(handler), "No query handler was registered!");
        }
    }
}
