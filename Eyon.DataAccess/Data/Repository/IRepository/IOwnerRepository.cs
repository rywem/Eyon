using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IOwnerRepository<TRecord, TRelation> 
        where TRecord : class, IRecord
        where TRelation : class, IOwner
    {
        void AddOwned( string ownerId, TRecord entity, TRelation relationEntity );
        TRecord GetFirstOrDefaultOwned(string ownerId, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null );

        IEnumerable<TRecord> GetAllOwned( string ownerId, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>,
                        IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null );

        void UpdateOwned( TRecord entity, string ownerId );
    }
}
