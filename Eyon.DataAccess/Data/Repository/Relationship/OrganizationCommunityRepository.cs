using System;
using System.Collections.Generic;
using System.Text;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class OrganizationCommunityRepository : Repository<OrganizationCommunity>, IOrganizationCommunityRepository
    {
        private readonly ApplicationDbContext _db;
        public OrganizationCommunityRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
