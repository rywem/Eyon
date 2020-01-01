using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class CommunityPostalCodeRepository : Repository<CommunityPostalCode>, ICommunityPostalCodeRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityPostalCodeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
