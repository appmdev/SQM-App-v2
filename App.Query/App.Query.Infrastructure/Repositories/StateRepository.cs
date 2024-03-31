using App.Query.Domain.Entities;
using App.Query.Domain.Repositories;
using App.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace App.Query.Infrastructure.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public StateRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task CreateAsync(StateEntity state)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.States.Add(state);

            _ = await context.SaveChangesAsync();
        }

        public async Task<StateEntity> GetByIdAsync(Guid stateId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.States.FirstOrDefaultAsync(x => x.StateId == stateId);
        }
    }
}
