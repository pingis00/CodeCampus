using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Models;
using CodeCampus.Infrastructure.Repositories;
using CodeCampus.Infrastructure.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Services;

public class AddressService(IAddressRepository addressRepository, UserManager<UserEntity> userManager, ILogger<AddressService> logger) : IAddressService
{
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ILogger<AddressService> _logger = logger;

    public async Task<AddressEntity> GetUserAddressAsync(string userId)
    {
        try
        {
            var addressResult = await _addressRepository.GetOneAsync(a => a.Users.Any(u => u.Id == userId));
            return addressResult.Status == StatusCode.OK ? (AddressEntity)addressResult.ContentResult! : null!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving user address for user {UserId}", userId);
            return null!;
        }
    }

    public async Task<ResponseResult> CreateOrUpdateAddressAsync(UserEntity user, AccountDetailsAddressInfoModel addressModel)
    {
        try
        {
            var existingAddress = await _addressRepository.GetOneAsync(a =>
                a.AddressLine_1 == addressModel.Addressline_1
                && a.PostalCode == addressModel.PostalCode
                && a.City == addressModel.City);

            if (existingAddress.Status != StatusCode.OK || ((AddressEntity)existingAddress.ContentResult!).AddressLine_2 != addressModel.Addressline_2)
            {
                var newAddress = AddressFactory.Create(addressModel.Addressline_1, addressModel.Addressline_2!, addressModel.PostalCode, addressModel.City);
                newAddress.Users.Add(user);
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
                if (!address.Users.Contains(user))
                {
                    address.Users.Add(user);
                    await _addressRepository.UpdateOneAsync(a => a.Id == address.Id, address);
                }
                user.AddressId = address.Id;
                await _userManager.UpdateAsync(user);
                return existingAddress;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateOrUpdateAddressAsync");
            return ResponseFactory.Error(ex.Message);
        }
    }
}
