using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Calc.Data.Interfaces;

namespace Calc.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected ApplicationDbContext _context;
    internal DbSet<T> _dbSet;
    protected readonly ILogger _logger;

    public GenericRepository(ApplicationDbContext context, ILogger logger)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _logger = logger;
    }

    public virtual async Task<IEnumerable<T>> All()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} All function error", typeof(T));
            return new List<T>();
        }
    }

    public virtual async Task<bool> Add(T entity)
    {
        try
        {
            _dbSet.Add(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Add function error", typeof(T));
            return false;
        }
    }

    public virtual async Task<bool> Delete(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Delete function error", typeof(T));
            return false;
        }
    }

    public virtual async Task<bool> Upsert(T entity)
    {
        try
        {
            if (_dbSet.Contains(entity))
            {
                _dbSet.Update(entity);
                return true;
            }

            _dbSet.Add(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Update function error", typeof(T));
            return false;
        }
    }
}