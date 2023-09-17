using System.Linq.Expressions;
using Taxify.Domain.Commons;

namespace Taxify.DataAccess.Contracts;

public interface IRepository<TEntity> where TEntity : Auditable
{
    ValueTask<TEntity> CreateAsync(TEntity entity);
    ValueTask<TEntity> Update(TEntity entity);
    ValueTask<bool> Delete(TEntity entity);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null,
        bool isTracking = true);
}
