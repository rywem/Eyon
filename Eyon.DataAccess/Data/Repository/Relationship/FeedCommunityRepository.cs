using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;
using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository
{
    public class FeedCommunityRepository : Repository<FeedCommunity>, IFeedCommunityRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedCommunityRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedCommunity entityToAdd )
        {
            if (_db.FeedCommunity.Any(x => x.FeedId == entityToAdd.FeedId && x.CommunityId == entityToAdd.CommunityId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  CommunityId {1}", entityToAdd.FeedId, entityToAdd.CommunityId)));

            base.Add(entityToAdd);
        }

        public FeedCommunity AddFromEntities( Feed firstEntity, Community secondEntity )
        {
            var newObj = new FeedCommunity()
            {
                FeedId = firstEntity.Id,
                CommunityId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
