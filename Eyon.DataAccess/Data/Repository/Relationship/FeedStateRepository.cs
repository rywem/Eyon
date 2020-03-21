using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;
using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository
{
    public class FeedStateRepository : Repository<FeedState>, IFeedStateRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedStateRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedState entityToAdd )
        {
            if (_db.FeedState.Any(x => x.FeedId == entityToAdd.FeedId && x.StateId == entityToAdd.StateId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  StateId {1}", entityToAdd.FeedId, entityToAdd.StateId)));

            base.Add(entityToAdd);
        }

        public FeedState AddFromEntities( Feed firstEntity, State secondEntity )
        {
            var newObj = new FeedState()
            {
                FeedId = firstEntity.Id,
                StateId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
