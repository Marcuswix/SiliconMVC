using Infrastructure.Repositories;
using Infrastructure.Model;
using System.Diagnostics;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;

namespace Infrastructure.Services
{
    public class UserServices
    {
        private readonly UserRepository _repository;

        public UserServices(UserRepository repository)
        {
            _repository = repository;
        }

        //CRUD

        //Create
        public async Task<RepositoriesResult> CreateUser(SignUpModel user)
        {
            try
            {
                var (password, securityKey) = PasswordHasher.GenerateSecurePassword(user.Password);

                if (user != null)
                {
                    var userToSignUp = new UserEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PasswordHash = password,
                        Created = DateTime.Now,
                        Updated = null!,
                    };

                    var existUser = await _repository.AlreadyExistAsync(x => x.Email == user.Email);

                    if (existUser.StatusCode == StatusCodes.NOT_FOUND)
                    {
                        var result = await _repository.CreateOneAsync(userToSignUp);
                        return ResponseFactory.Ok(result);
                    }

                    else
                    {
                        return ResponseFactory.AlreadyExist();
                    }
                }

                return ResponseFactory.Error();
            }

            catch (Exception ex)
            {
                Debug.WriteLine("CreateUser" + ex.Message);
                return ResponseFactory.Error();
            }
        }

        public async Task<UserModel> SignInUserAsync(SignInModel user)
        {
            try
            {
                if (user != null)
                {
                    var result = await _repository.GetOneUserAsync(user);

                    if (result.StatusCode == StatusCodes.OK && result != null!)
                    {
                        UserEntity userEntity = (UserEntity)result.ContentResult!;

                            return new UserModel
                            {
                                Id = userEntity.Id,
                                FirstName = userEntity.FirstName,
                                LastName = userEntity.LastName,
                                Email = userEntity.Email,
                                Biography = userEntity.Biography ?? string.Empty,
                                Phone = userEntity.PhoneNumber ?? string.Empty,
                            };
                    }
                }
                return null!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SignInUserAsync" + ex.Message);
                return null!;

            }
        }

        public async Task<RepositoriesResult> UpdateUserInfo(UserModel model)
        {
            try
            {
                if (model != null)
                {
                    var entityUpdate = new UserEntity
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Biography = model.Biography ?? string.Empty,
                    };

                    var result = await _repository.UpdateOneAsync(x => x.Email == model.Email, entityUpdate);

                    if (result.StatusCode == StatusCodes.OK) 
                    {
                        return ResponseFactory.Ok(result);
                    }
                    else
                    {
                        return ResponseFactory.Error();
                    }
                }

                return ResponseFactory.Error();
            }
            catch (Exception ex){ Debug.WriteLine("UpdateUserInfo" + ex.Message);
            return ResponseFactory.Error(); ;
            }
        }
    }
}
