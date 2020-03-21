using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;
using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository
{
    public class FeedProfileRepository : Repository<FeedProfile>, IFeedProfileRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedProfileRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedProfile entityToAdd )
        {
            if (_db.FeedProfile.Any(x => x.FeedId == entityToAdd.FeedId && x.ProfileId == entityToAdd.ProfileId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  ProfileId {1}", entityToAdd.FeedId, entityToAdd.ProfileId)));

            base.Add(entityToAdd);
        }

        public FeedProfile AddFromEntities( Feed firstEntity, Profile secondEntity )
        {
            var newObj = new FeedProfile()
            {
                FeedId = firstEntity.Id,
                ProfileId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
