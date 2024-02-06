using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Razor_Rentals.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _appDbCtx;
        internal DbSet<T> genericDbSet;
        public GenericRepository(ApplicationDbContext appDbCtx)
        {
            _appDbCtx = appDbCtx;
            genericDbSet = _appDbCtx.Set<T>();
        }
        public virtual void Add(T entity)
        {
            genericDbSet.Add(entity);
        }
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = genericDbSet;
            query = query.Where(predicate);
            return query.FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            IQueryable<T> query = genericDbSet;
            return query.ToList();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = genericDbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual void Remove(T id)
        {
            genericDbSet.Remove(id);
        }



        public virtual void SaveChanges()
        {
            _appDbCtx.SaveChanges();
        }
        public virtual void SetEntityState(T entity, EntityState state)
        {
            genericDbSet.Entry(entity).State = state;
        }

        public virtual T Update(T entity)
        {
            return genericDbSet.Update(entity).Entity;
        }
    }
}
