using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository
{
    /// <summary>
    /// FeedRepository inherits from PrivacyRepository. 
    /// </summary>
    /// <remarks>
    /// Privacy information stored on the feed object is a denormalization 
    /// of data and is not the primary information location. Privacy 
    /// information is also stored on the base object's table. 
    /// </remarks>
    public class FeedRepository : PrivacyRepository<Feed, ApplicationUserFeed>, IFeedRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public override void Add( Feed entity )
        {            
            entity.CreationDateTime = DateTime.Now.ToUniversalTime();
            entity.ModifiedDateTime = entity.CreationDateTime;
            base.Add(entity);
        }
        public void UpdateIfOwner( string currentUserId, Feed feed )
        {
            var objFromDb = ( from r in _db.Feed
                              join a in _db.ApplicationUserRecipe on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == feed.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                throw new SafeException("An error ocurred.", new Exception(string.Format("Ownership relationship not found on record. currentUserId {0},  recipe.Id {1}", currentUserId, feed.Id)));

            objFromDb.Privacy = feed.Privacy;
            objFromDb.Text = feed.Text;
            objFromDb.ModifiedDateTime = DateTime.Now.ToUniversalTime();
            dbSet.Update(objFromDb);
        }
    }
}
