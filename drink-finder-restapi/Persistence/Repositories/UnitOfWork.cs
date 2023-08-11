using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Persistence.Contexts;

namespace drink_finder_restapi.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DFDbContext _context;

        public UnitOfWork(DFDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
