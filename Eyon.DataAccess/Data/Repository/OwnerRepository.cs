using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eyon.DataAccess.Data.Repository
{
    public class OwnerRepository<TRecord, TRelation> : Repository<TRecord> , IOwnerRepository<TRecord, TRelation>
        where TRecord : class, IRecord
        where TRelation : class, IOwner
    {

        //protected readonly DbContext Context;
        internal DbSet<TRelation> dbSetRelation;

        public OwnerRepository( DbContext context ) : base(context)
        {
            this.dbSetRelation = context.Set<TRelation>();
        }

        public void AddOwned( string ownerId, TRecord entity )
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TRecord> GetAllOwned( string ownerId, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>, IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null )
        {
            IQueryable<TRecord> query = dbSet;

            query = from e in dbSet
                    join k in dbSetRelation on e.Id equals k.ObjectId
                    where k.ApplicationUserId.Equals(ownerId)
                    select e;

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

            return query.ToList();
        }

        public TRecord GetFirstOrDefaultOwned( string ownerId, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null )
        {
            throw new NotImplementedException();
        }

        public void UpdateOwned( TRecord entity, string ownerId )
        {
            throw new NotImplementedException();
        }
    }
}
