using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Models;
using CodeCampus.Infrastructure.Repositories;
using CodeCampus.Infrastructure.Responses;
using Microsoft.AspNetCore.Identity;

namespace CodeCampus.Infrastructure.Services;

public class AddressService(AddressRepository addressRepository, UserManager<UserEntity> userManager)
{
    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<AddressEntity> GetUserAddressAsync(string userId)
    {
        var addressResult = await _addressRepository.GetOneAsync(a => a.UserId == userId);
        return addressResult.Status == StatusCode.OK ? (AddressEntity)addressResult.ContentResult! : null!;
    }

    public async Task<ResponseResult> CreateOrUpdateAddressAsync(UserEntity user, AccountDetailsAddressInfoModel addressModel)
    {
        try
        {
            var existingAddress = await _addressRepository.GetOneAsync(a => a.AddressLine_1 == addressModel.Addressline_1
                                                                            && a.PostalCode == addressModel.PostalCode
                                                                            && a.City == addressModel.City);

            if (existingAddress.Status != StatusCode.OK || ((AddressEntity)existingAddress.ContentResult!).AddressLine_2 != addressModel.Addressline_2)
            {
                var newAddress = new AddressEntity
                {
                    UserId = user.Id,
                    AddressLine_1 = addressModel.Addressline_1,
                    AddressLine_2 = addressModel.Addressline_2,
                    PostalCode = addressModel.PostalCode,
                    City = addressModel.City,
                };

                var createResult = await _addressRepository.CreateOneAsync(newAddress);
                if (createResult.Status == StatusCode.OK)
                {
                    var createdAddress = (AddressEntity)createResult.ContentResult!;
                    user.AddressId = createdAddress.Id;
                    await _userManager.UpdateAsync(user);
                }
                return createResult;
            }
            else
            {
                var address = (AddressEntity)existingAddress.ContentResult!;
                user.AddressId = address.Id;
                await _userManager.UpdateAsync(user);
                return existingAddress;
            }
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}
