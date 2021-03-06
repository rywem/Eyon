﻿using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IOwnerRepository<TRecord, TOwnerRelation> 
        where TRecord : class, IRecord
        where TOwnerRelation : class, IOwnerApplicationUserRelationship
    {
        void AddOwnerRelationship( string ownerId, TRecord entity, TOwnerRelation relationEntity );

        bool IsOwner( string userIdToCheck, long entityId );
        Task<bool> IsOwnerAsync( string userIdToCheck, long entityId );
        TRecord GetFirstOrDefaultOwned(string ownerId, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null, bool tracking = true );
        Task<TRecord> GetFirstOrDefaultOwnedAsync( string ownerId, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null, bool tracking = true );

        IEnumerable<TRecord> GetAllOwned( string ownerId, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>,
                        IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true );
        Task<IEnumerable<TRecord>> GetAllOwnedAsync( string ownerId, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>,
                        IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true );
        
    }
}
