using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;

namespace Eyon.DataAccess.Data.Repository
{
    public class FeedCountryRepository : Repository<FeedCountry>, IFeedCountryRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedCountryRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedCountry entityToAdd )
        {
            if (_db.FeedCountry.Any(x => x.FeedId == entityToAdd.FeedId && x.CountryId == entityToAdd.CountryId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  CountryId {1}", entityToAdd.FeedId, entityToAdd.CountryId)));

            base.Add(entityToAdd);
        }
    }
}
