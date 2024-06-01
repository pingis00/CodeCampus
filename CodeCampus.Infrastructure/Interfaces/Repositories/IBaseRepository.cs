using CodeCampus.Infrastructure.Responses;
using System.Linq.Expressions;

namespace CodeCampus.Infrastructure.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<ResponseResult> CreateOneAsync(TEntity entity);
    Task<ResponseResult> GetAllAsync();
    Task<ResponseResult> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate);
    Task<ResponseResult> UpdateOneAsync(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity);
    Task<ResponseResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate);
    Task<ResponseResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> predicate);
}
