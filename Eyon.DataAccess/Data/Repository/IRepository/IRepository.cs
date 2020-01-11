using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //IQueryable<T> ApplyQueryFilters( Expression<Func<T, bool>> filter = null, string includeProperties = null, bool tracking = true );
        T Get(long id );

        Task<T> GetAsync( long id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
                        IOrderedQueryable<T>> orderBy = null, string includeProperties = null );
        Task<IEnumerable<T>> GetAllAsync( Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
                        IOrderedQueryable<T>> orderBy = null, string includeProperties = null );

        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool tracking = true);

        Task<T> GetFirstOrDefaultAsync( Expression<Func<T, bool>> filter = null, string includeProperties = null, bool tracking = true );

        bool Any(Expression<Func<T, bool>> filter);
        Task<bool> AnyAsync( Expression<Func<T, bool>> filter );

        void Add(T entity);        

        void Remove(long id);        
        void Remove(T entity);
        
        void RemoveRange(IEnumerable<T> entitiesToRemove);
    }
}
