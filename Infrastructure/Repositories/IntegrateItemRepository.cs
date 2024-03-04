
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class IntegrateItemRepository : BaseRepository<IntegrateItemRepository>
    {
        public IntegrateItemRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
