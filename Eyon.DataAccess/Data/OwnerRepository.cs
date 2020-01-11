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
namespace Eyon.DataAccess.Data
{
    public class OwnerRepository<TRecord, TRelation> : Repository<TRecord> , IOwnerRepository<TRecord, TRelation>
        where TRecord : class, IRecord
        where TRelation : class, IOwner
    {
        internal DbSet<TRelation> dbSetRelation;

        public OwnerRepository( DbContext context ) : base(context)
        {
            this.dbSetRelation = context.Set<TRelation>();
        }

        public void AddOwnerRelationship( string ownerId, TRecord addedEntity, TRelation relationEntity )
        {            
            DbSet<ApplicationUser> userDbSet = Context.Set<ApplicationUser>();
            var userFromDb = userDbSet.FirstOrDefault(x => x.Id.Equals(ownerId));
            var entityFromDb = dbSet.FirstOrDefault(c => c.Id == addedEntity.Id);
            if ( userFromDb == null || entityFromDb == null || entityFromDb.Id == 0 )
                throw new WebUserSafeException("An error occurred.");
            else
            {         
                // TODO Refactor
                relationEntity.ApplicationUserId = userFromDb.Id;
                relationEntity.ObjectId = entityFromDb.Id;
                dbSetRelation.Add(relationEntity);
            }
        }

        public IEnumerable<TRecord> GetAllOwned( string ownerId, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>, IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<TRecord> query = dbSet;

            query = from e in dbSet
                    join k in dbSetRelation on e.Id equals k.ObjectId
                    where k.ApplicationUserId.Equals(ownerId)
                    select e;
            
            return ApplyQueryFilters(query, filter, includeProperties, tracking).ToList();
        }


        /// <summary>
        /// GetAllAvailable considers if the items are owned. 
        /// </summary>        
        public async Task<IEnumerable<TRecord>> GetAllOwnedAsync( string ownerId, Expression<Func<TRecord, bool>> filter = null, Func<IQueryable<TRecord>, IOrderedQueryable<TRecord>> orderBy = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<TRecord> query = dbSet;

            query = from e in dbSet
                    join k in dbSetRelation on e.Id equals k.ObjectId
                    where k.ApplicationUserId.Equals(ownerId)
                    select e;
            
            return await ApplyQueryFilters(query, filter, includeProperties, tracking).ToListAsync();
        }

        public TRecord GetFirstOrDefaultOwned( string ownerId, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<TRecord> query = dbSet;

            query = from e in dbSet
                    join k in dbSetRelation on e.Id equals k.ObjectId
                    where k.ApplicationUserId.Equals(ownerId)
                    select e;
            
            return ApplyQueryFilters(query, filter, includeProperties, tracking).FirstOrDefault();
        }
        public bool IsOwner( string userIdToCheck, long entityId )
        {                    
            var result = dbSet.Where(x => x.Id == entityId)
                             .Join(dbSetRelation.Where(x=> x.ApplicationUserId.Equals(userIdToCheck)), 
                             d => d.Id, 
                             dr => dr.ObjectId,
                            (record, relation) => new 
                            {
                                entityId = record.Id,
                                applicationUserId = relation.ApplicationUserId
                            }).Any(x => x.entityId > 0 && !string.IsNullOrEmpty(x.applicationUserId));

            return result;
        }

        public async Task<bool> IsOwnerAsync( string userIdToCheck, long entityId )
        {
            var result = await dbSet.Where(x => x.Id == entityId)
                             .Join(dbSetRelation.Where(x => x.ApplicationUserId.Equals(userIdToCheck)),
                             d => d.Id,
                             dr => dr.ObjectId,
                            ( record, relation ) => new
                            {
                                entityId = record.Id,
                                applicationUserId = relation.ApplicationUserId
                            }).AnyAsync(x => x.entityId > 0 && !string.IsNullOrEmpty(x.applicationUserId));

            return result;
        }



        public async Task<TRecord> GetFirstOrDefaultOwnedAsync( string ownerId, Expression<Func<TRecord, bool>> filter = null, string includeProperties = null, bool tracking = true )
        {
            IQueryable<TRecord> query = dbSet;           
            return await ApplyQueryFilters(query, filter, includeProperties, tracking).FirstOrDefaultAsync();
        }
    }
}
