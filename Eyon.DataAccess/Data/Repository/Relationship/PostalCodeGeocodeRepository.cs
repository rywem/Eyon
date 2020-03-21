using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class PostalCodeGeocodeRepository : Repository<PostalCodeGeocode>, IPostalCodeGeocodeRepository
    {
        private readonly ApplicationDbContext _db;

        public PostalCodeGeocodeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public PostalCodeGeocode AddFromEntities( PostalCode firstEntity, Geocode secondEntity )
        {
            var newObj = new PostalCodeGeocode()
            {
                PostalCodeId = firstEntity.Id,
                GeocodeId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
