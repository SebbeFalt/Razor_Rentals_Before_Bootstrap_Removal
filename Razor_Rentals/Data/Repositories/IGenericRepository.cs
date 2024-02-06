using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Razor_Rentals.Data.Repositories
{

    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        T Update(T entity);
        T Get(Expression<Func<T, bool>> predicate);
        void Remove(T id);
        IEnumerable<T> GetAll();
        void SaveChanges();
        void SetEntityState(T entity, EntityState state);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);


    }

}
