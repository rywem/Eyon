using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data
{
    public class Repository<T> : Eyon.DataAccess.Data.Repository.IRepository.IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.Context = context;
            this.dbSet = context.Set<T>();
        }


        public async virtual Task<T> GetAsync(long id )
        {
            return await dbSet.FindAsync(id);
        }

        public T Get(long id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            // include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync( Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null )
        {
            IQueryable<T> query = dbSet;

            if ( filter != null )
            {
                query = query.Where(filter);
            }
            // include properties will be comma seperated
            if ( includeProperties != null )
            {
                foreach ( var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) )
                {
                    query = query.Include(includeProperty);
                }
            }

            if ( orderBy != null )
            {
                return orderBy(query).ToList();
            }
            return await query.ToListAsync();
        }


        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            // include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if ( tracking == false )
                query.AsNoTracking();
            return query.FirstOrDefault();
        }

        public async Task<T> GetFirstOrDefaultAsync( Expression<Func<T, bool>> filter = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<T> query = dbSet;

            if ( filter != null )
            {
                query = query.Where(filter);
            }
            // include properties will be comma seperated
            if ( includeProperties != null )
            {
                foreach ( var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) )
                {
                    query = query.Include(includeProperty);
                }
            }
            if ( tracking == false )
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }
        public bool Any( Expression<Func<T, bool>> filter )
        {
            IQueryable<T> query = dbSet;
            return query.Any(filter);
        }

        public async Task<bool> AnyAsync( Expression<Func<T, bool>> filter )
        {
            IQueryable<T> query = dbSet;
            return await query.AnyAsync(filter);
        }

        public virtual void Add( T entity )
        {
            dbSet.Add(entity);
        }

        public void Remove(long id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entitiesToRemove)
        {
            dbSet.RemoveRange(entitiesToRemove);
        }
    }
}
