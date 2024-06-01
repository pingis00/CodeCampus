using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Repositories;

public interface ISubscribeRepository : IBaseRepository<SubscribeEntity>
{
    Task<ResponseResult> IsEmailSubscribedAsync(string email);
}
