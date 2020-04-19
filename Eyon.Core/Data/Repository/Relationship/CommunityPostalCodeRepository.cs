using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Core.Data.Repository
{
    public class CommunityPostalCodeRepository : Repository<CommunityPostalCode>, ICommunityPostalCodeRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityPostalCodeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public CommunityPostalCode AddFromEntities( Community community, PostalCode postalcode )
        {
            var newObj = new CommunityPostalCode()
            {
                CommunityId = community.Id,
                PostalCodeId = postalcode.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
