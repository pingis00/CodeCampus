using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Models;
using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface IAddressService
{
    Task<AddressEntity> GetUserAddressAsync(string userId);
    Task<ResponseResult> CreateOrUpdateAddressAsync(UserEntity user, AccountDetailsAddressInfoModel addressModel);
}
