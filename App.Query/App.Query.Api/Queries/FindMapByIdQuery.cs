using CQRS.Core.Queries;

namespace App.Query.Api.Queries
{
    public class FindMapByIdQuery: BaseQuery
    {
        public Guid Id { get; set; }
    }
}
