using Eyon.Core.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;
using Eyon.Models;

namespace Eyon.Core.Data.Repository
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

        public FeedCountry AddFromEntities( Feed firstEntity, Country secondEntity )
        {
            var newObj = new FeedCountry()
            {
                FeedId = firstEntity.Id,
                CountryId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
