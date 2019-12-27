using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.Relationship
{
    public class OrganizationCookbooksRepository : Repository<OrganizationCookbooks>, IOrganizationCookbooksRepository
    {
        private readonly ApplicationDbContext _db;
        public OrganizationCookbooksRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
