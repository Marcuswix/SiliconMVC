using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;

namespace Infrastructure.Repositories
{
    public class UserRepository(DataContext dataContext) : BaseRepository<UserEntity>(dataContext)
    {
        private readonly DataContext _dataContext = dataContext;

        public override async Task<RepositoriesResult> GetAllAsync()
        {
            try
            {
                var result = await _dataContext.Users
                    .Include(x => x.Address)
                    .ToListAsync();

                if(result != null)
                {
                    return ResponseFactory.Ok(result);
                }
                    return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllAsyncUsers" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<RepositoriesResult> GetOneUserAsync(SignInModel model)
        {
            try
            {
                var result = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (result != null)
                {
                    return ResponseFactory.Ok(result);
                }

                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetOneAsyncUsers" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<RepositoriesResult> GetUserInfoAsync(UserModel model)
        {
            try
            {
                var result = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (result != null)
                {
                    return ResponseFactory.Ok(result);
                }

                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetUserInfoAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
