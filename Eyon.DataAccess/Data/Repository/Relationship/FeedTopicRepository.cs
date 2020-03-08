using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;

namespace Eyon.DataAccess.Data.Repository
{
    public class FeedTopicRepository : Repository<FeedTopic>, IFeedTopicRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedTopicRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedTopic entityToAdd )
        {
            if (_db.FeedTopic.Any(x => x.FeedId == entityToAdd.FeedId && x.TopicId == entityToAdd.TopicId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  TopicId {1}", entityToAdd.FeedId, entityToAdd.TopicId)));

            base.Add(entityToAdd);
        }
    }
}
