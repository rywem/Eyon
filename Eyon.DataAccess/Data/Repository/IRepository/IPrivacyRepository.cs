using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IPrivacyRepository<TRecord, TOwnerRelation> : IOwnerRepository<TRecord, TOwnerRelation>
        where TRecord : class, IPrivacy
        where TOwnerRelation : class, IOwnerApplicationUserRelationship
    {
        bool UserCanView( string userIdToCheck, long entityId );

        Task<bool> UserCanViewAsync( string userIdToCheck, long entityId );
        TRecord GetFirstOrDefaultAvailable(string userIdToCheck, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null, bool tracking = true );
        Task<TRecord> GetFirstOrDefaultAvailableAsync( string userIdToCheck, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null, bool tracking = true );

        IEnumerable<TRecord> GetAllAvailable( string userIdToCheck, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>,
                        IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true );
        Task<IEnumerable<TRecord>> GetAllAvailableAsync( string userIdToCheck, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>,
                        IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true );

        IEnumerable<TRecord> GetAllPublic( Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>,
                        IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true );
        Task<IEnumerable<TRecord>> GetAllPublicAsync( Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>,
                        IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true );
    }
}
