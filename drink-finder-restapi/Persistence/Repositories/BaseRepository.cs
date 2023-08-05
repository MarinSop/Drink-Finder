using drink_finder_restapi.Persistence.Contexts;

namespace drink_finder_restapi.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DFDbContext _context;

        public BaseRepository(DFDbContext context)
        {
            _context = context;
        }
    }
}
