using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Responses;
using System.Linq.Expressions;

namespace CodeCampus.Infrastructure.Interfaces.Repositories;

public interface IUserCourseRepository : IBaseRepository<UserCourseEntity>
{
    Task<ResponseResult> DeleteAllAsync(Expression<Func<UserCourseEntity, bool>> predicate);
}
