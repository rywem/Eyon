using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;

namespace Eyon.DataAccess.Data.Repository
{
    public class FeedCategoryRepository : Repository<FeedCategory>, IFeedCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedCategoryRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedCategory entityToAdd )
        {
            if (_db.FeedCategory.Any(x => x.FeedId == entityToAdd.FeedId && x.CategoryId == entityToAdd.CategoryId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  CategoryId {1}", entityToAdd.FeedId, entityToAdd.CategoryId)));

            base.Add(entityToAdd);
        }
    }
}
