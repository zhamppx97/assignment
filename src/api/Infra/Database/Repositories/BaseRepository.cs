using api.Core;
using api.Domain;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace api.Infra;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DataContext _context;

    public BaseRepository(DataContext context)
    {
        this._context = context;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<bool> DoesExist(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        entity.UserId = Guid.NewGuid();
        entity.CreateDate = DateTime.Now;
        entity.UpdateDate = entity.CreateDate;
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        entity.UpdateDate = DateTime.Now;
        return entity;
    }

    public TEntity Remove(TEntity entity)
    {
        entity.UpdateDate = DateTime.Now;
        return entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
