using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Model;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Infrastructure.Services
{
    public class AccountServices
    {
        private readonly UserRepository _userRepository;

        public AccountServices(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RepositoriesResult> GetUserInformation(UserModel model)
        {
            try
            {
                var result = await _userRepository.GetOneAsync(x => x.Email == model.Email);
                return ResponseFactory.Ok(result);
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error();
            }
        }

        //public async Task<RepositoriesResult> UpdateUserInformation(UserModel model)
        //{
        //    try
        //    {
        //        var reso = new UserEntity
        //        {  Email = model.Email,
        //           FirstName = model.FirstName,
        //           LastName = model.LastName,
        //           Phone = model.Phone,
        //        };
        //        var result = await _userRepository.UpdateOneAsync(x => x.Email == model.Email, reso);
        //        return ResponseFactory.Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseFactory.Error();
        //    }
        //}
    }
}
