
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<AddressEntity>
    {
        public AddressRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
