using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Core.Data.Repository
{
    public class CommunityGeocodeRepository : Repository<CommunityGeocode>, ICommunityGeocodeRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityGeocodeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public CommunityGeocode AddFromEntities( Community community, Geocode geocode )
        {
            var newObj = new CommunityGeocode()
            {
                CommunityId = community.Id,
                GeocodeId = geocode.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
