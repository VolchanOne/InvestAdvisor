using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InvestAdvisor.Data.Contracts;

namespace InvestAdvisor.Data
{
    public abstract class GenericRepository<TContext, TEntity> :
        IGenericRepository<TEntity> where TEntity : class where TContext : DbContext, new()
    {
        protected TContext _context { get; set; } = new TContext();
        protected DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public IQueryable<TEntity> Get()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).AsQueryable();
        }
        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate);
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query =_dbSet;
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
