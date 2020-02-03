using BookStore.Domain.Entities;
using BookStore.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class CrudService<TEntity, TContext> : ICrudService<TEntity> 
        where TEntity : BaseEntity 
        where TContext: DbContext
    {
        protected readonly TContext dbContext;

        public CrudService(TContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbContext.Set<TEntity>().AddRangeAsync(entities).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual  async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            return query;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbContext.Set<TEntity>().Where(e => e.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().UpdateRange(entities);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
