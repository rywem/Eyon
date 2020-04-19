using Eyon.Core.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;
using Eyon.Models;

namespace Eyon.Core.Data.Repository
{
    public class FeedUserImageRepository : Repository<FeedUserImage>, IFeedUserImageRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedUserImageRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedUserImage entityToAdd )
        {
            if (_db.FeedUserImage.Any(x => x.FeedId == entityToAdd.FeedId && x.UserImageId == entityToAdd.UserImageId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedUserImage already exists. FeedId {0},  UserImageId {1}", entityToAdd.FeedId, entityToAdd.UserImageId)));

            base.Add(entityToAdd);
        }

        public FeedUserImage AddFromEntities( Feed firstEntity, UserImage secondEntity )
        {
            var newObj = new FeedUserImage()
            {
                FeedId = firstEntity.Id,
                UserImageId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
