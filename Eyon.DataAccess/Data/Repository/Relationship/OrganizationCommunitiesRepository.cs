using System;
using System.Collections.Generic;
using System.Text;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class OrganizationCommunitiesRepository : Repository<OrganizationCommunities>, IOrganizationCommunitiesRepository
    {
        private readonly ApplicationDbContext _db;
        public OrganizationCommunitiesRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
