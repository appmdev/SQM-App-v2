
using App.Query.Domain.Entities;

namespace App.Query.Domain.Repositories
{
    public interface IStateRepository
    {
        Task CreateAsync(StateEntity state);
        Task<StateEntity> GetByIdAsync(Guid stateId);
    }
}
