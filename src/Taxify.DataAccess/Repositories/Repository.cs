using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Taxify.DataAccess.Contexts;
using Taxify.DataAccess.Contracts;
using Taxify.Domain.Commons;

namespace Taxify.DataAccess.Repositories;

public class Repository<TEntity>:IRepository<TEntity> where TEntity : Auditable
{
    private readonly TaxifyDbContext _context;
    private readonly DbSet<TEntity> _set;

    public Repository(TaxifyDbContext context)
    {
        _context = context;
        _set = _context.Set<TEntity>();
    }

    public async ValueTask CreateAsync(TEntity entity)
    {
        await _set.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Update(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        _context.Update(entity).State = EntityState.Deleted;
    }

    public void Destroy(TEntity entity)
    {
        _context.Remove(entity);
    }

    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
    {
        IQueryable<TEntity> entities = expression == null ? _set.AsQueryable() : _set.Where(expression).AsQueryable();
        
        if(includes!=null)
            foreach (var include in includes)
                entities = entities.Include(include);

        return await entities.Where(t => !t.IsDeleted).FirstOrDefaultAsync();
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true)
    {
        IQueryable<TEntity> entities = expression == null ? _set.AsQueryable() : _set.Where(expression).AsQueryable();

        entities = isTracking ? entities.AsNoTracking() : entities;
        
        if(includes!=null)
            foreach (var include in includes)
                entities = entities.Include(include);

        return entities.Where(t => !t.IsDeleted);
    }
}