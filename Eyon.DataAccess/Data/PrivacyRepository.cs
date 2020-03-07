using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eyon.Models.Enums;
namespace Eyon.DataAccess.Data
{
    public class PrivacyRepository<TRecord, TRelation> : OwnerRepository<TRecord, TRelation> , IPrivacyRepository<TRecord, TRelation>
        where TRecord : class, IPrivacy
        where TRelation : class, IOwnerApplicationUserRelationship
    {        

        public PrivacyRepository( DbContext context ) : base(context)
        {
            this.dbSetRelation = context.Set<TRelation>();
        }      
        
        public bool UserCanView( string userIdToCheck, long entityId )
        {
            if ( base.IsOwner(userIdToCheck, entityId) == true )
                return true;
            else
            {
                var objFromDb = base.GetFirstOrDefault(x => x.Id == entityId);

                if ( objFromDb != null && objFromDb.Privacy == Models.Enums.Privacy.Public )
                    return true;
            }
            return false;

        }
        public async Task<bool> UserCanViewAsync( string userIdToCheck, long entityId )
        {
            if ( await base.IsOwnerAsync(userIdToCheck, entityId) == true )
                return true;
            else
            {
                var objFromDb = await base.GetFirstOrDefaultAsync(x => x.Id == entityId);

                if ( objFromDb != null && objFromDb.Privacy == Models.Enums.Privacy.Public )
                    return true;
            }
            return false;
        }
        /// <summary>
        /// This will return a record if the userIdToCheck is the owner, or if the desired record's Privacy is public
        /// </summary>        
        public TRecord GetFirstOrDefaultAvailable( string userIdToCheck, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null, bool tracking = true )
        {            
            IQueryable<TRecord> query = dbSet;

            query = from m in dbSet
                    join d in dbSetRelation on m.Id equals d.ObjectId into ps
                    from k in ps.DefaultIfEmpty()
                    let sort = ps == null ? 1 : 0
                    orderby sort
                    where ( k.ApplicationUserId.Equals(userIdToCheck) || m.Privacy == Privacy.Public )
                    select m;

            return ApplyQueryFilters(query, filter, includeProperties, tracking).FirstOrDefault();
        }
        /// <summary>
        /// This will return a record if the userIdToCheck is the owner, or if the desired record's privacy is public
        /// </summary>   
        public async Task<TRecord> GetFirstOrDefaultAvailableAsync( string userIdToCheck, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null, bool tracking = true )
        {                        
            IQueryable<TRecord> query = dbSet;

            query = from m in dbSet
                    join d in dbSetRelation on m.Id equals d.ObjectId into ps
                    from k in ps.DefaultIfEmpty()
                    let sort = ps == null ? 1 : 0
                    orderby sort
                    where ( k.ApplicationUserId.Equals(userIdToCheck) || m.Privacy == Privacy.Public )
                    select m;

            return await ApplyQueryFilters(query, filter, includeProperties, tracking).FirstOrDefaultAsync();
        }
       
        public IEnumerable<TRecord> GetAllAvailable( string userIdToCheck, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>, IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<TRecord> query = dbSet;
            query = from e in dbSet
                    join k in dbSetRelation on e.Id equals k.ObjectId into ps
                    from k in ps.DefaultIfEmpty()
                    where ( k.ApplicationUserId.Equals(userIdToCheck) || e.Privacy == Privacy.Public )
                    select e;
            return ApplyQueryFilters(query, filter, includeProperties, tracking).ToList();
        }

        public async Task<IEnumerable<TRecord>> GetAllAvailableAsync( string userIdToCheck, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>, IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<TRecord> query = dbSet;
            query = from e in dbSet
                    join k in dbSetRelation on e.Id equals k.ObjectId into ps
                    from k in ps.DefaultIfEmpty()
                    where ( k.ApplicationUserId.Equals(userIdToCheck) || e.Privacy == Privacy.Public )
                    select e;
            return await ApplyQueryFilters(query, filter, includeProperties, tracking).ToListAsync();
        }

        public IEnumerable<TRecord> GetAllPublic( Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>, IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<TRecord> query = dbSet;

            query = from e in dbSet
                    where e.Privacy == Privacy.Public
                    select e;

            return ApplyQueryFilters(query, filter, includeProperties, tracking).ToList();
        }

        public async Task<IEnumerable<TRecord>> GetAllPublicAsync( Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>, IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<TRecord> query = dbSet;

            query = from e in dbSet
                    where e.Privacy == Privacy.Public
                    select e;

            return await ApplyQueryFilters(query, filter, includeProperties, tracking).ToListAsync();
        }
    }
}
