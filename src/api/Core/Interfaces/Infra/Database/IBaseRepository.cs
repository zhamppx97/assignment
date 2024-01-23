using api.Domain;
using System.Linq.Expressions;

namespace api.Core;

public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<bool> DoesExist(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> InsertAsync(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TEntity entity);
    Task SaveChangesAsync();
}
