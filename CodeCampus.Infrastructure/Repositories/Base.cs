using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CodeCampus.Infrastructure.Repositories;

public abstract class Base<TEntity>(DataContext context, ILogger<Base<TEntity>> logger) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly ILogger<Base<TEntity>> _logger = logger;

    public virtual async Task<ResponseResult> CreateOneAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return ResponseFactory.Ok(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating entity");
            return ResponseFactory.Error(ex.Message);
        }

    }

    public virtual async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<TEntity> result = await _context.Set<TEntity>().ToListAsync();
            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all entities");
            return ResponseFactory.Error(ex.Message);
        }

    }

    public virtual async Task<ResponseResult> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            IEnumerable<TEntity> result = await _context.Set<TEntity>().Where(predicate).ToListAsync();
            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving entities with predicate");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result == null)
                return ResponseFactory.NotFound();

            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving a single entity with predicate");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> UpdateOneAsync(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();
                return ResponseFactory.Ok(result);
            }

            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating entity");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                _context.Set<TEntity>().Remove(result);
                await _context.SaveChangesAsync();
                return ResponseFactory.Ok("Successfully removed");
            }

            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting entity");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().AnyAsync(predicate);
            if (result)
            {
                return ResponseFactory.Exists();
            }

            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if entity already exists");
            return ResponseFactory.Error(ex.Message);
        }
    }
}

