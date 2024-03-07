
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<AddressEntity>
    {
        private readonly UserContext _userContext;

        public AddressRepository(UserContext userContext) : base(userContext)
        {
            _userContext = userContext;
        }

        public async Task<AddressEntity> GetOneAsync(UserEntity entity)
        {
            try
            {
                if (entity.Id != null)
                {
                    var result = await _userContext.Addresses.FirstOrDefaultAsync(x => x.Users == entity);
                    
                    if(result != null)
                    {
                        return result;
                    }
                }
                return null!;
            }
            catch (Exception ex)
            { Debug.WriteLine("GetAddress" + ex.Message);
                return null!;
                 }
        }
    }
}
