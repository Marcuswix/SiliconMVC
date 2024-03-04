﻿using Infrastructure.Entities;
using Infrastructure.Model;
using Infrastructure.Repositories;
using Infrastructure.Factories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class AddressServices
    {
        private readonly AddressRepository _repository;

        public AddressServices(AddressRepository repository)
        {
            _repository = repository;
        }

        //Create
        public async Task<RepositoriesResult> CreateAddress(AddressModel address)
        {
            try
            {
                if(address != null)
                {
                    var addressToCreate = new AddressEntity
                    {
                        StreetName = address.StreetName,
                        PostalCode = address.PostalCode,
                        City = address.City,
                    };

                    var exists = await _repository.AlreadyExistAsync(x => x.StreetName == address.StreetName && x.PostalCode == address.PostalCode && x.City == address.City);

                    if(exists == null)
                    {
                        var result = await _repository.CreateOneAsync(addressToCreate);

                        if (result.StatusCode == StatusCodes.OK)
                        {
                            return ResponseFactory.Ok(result);
                        }
                    }

                    else if(exists != null)
                    {
                        return ResponseFactory.AlreadyExist();
                    }

                }
                return ResponseFactory.Error();
            }
            catch (Exception ex)
            { Debug.WriteLine("CreateUser" + ex.Message);
                return ResponseFactory.Error();
            }
        }

        public async Task<RepositoriesResult> GetAllAddresses()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                if (result != null)
                {
                    return result;
                }

                return ResponseFactory.NotFound("The list is Empty");
            }
            catch (Exception ex) { Debug.WriteLine("CreateUser" + ex.Message);
            return ResponseFactory.Error();
            }
        }
    }
}