using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class OrganizationCookbookRepository : Repository<OrganizationCookbook>, IOrganizationCookbookRepository
    {
        private readonly ApplicationDbContext _db;
        public OrganizationCookbookRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public OrganizationCookbook AddFromEntities( Organization firstEntity, Cookbook secondEntity )
        {
            var newObj = new OrganizationCookbook()
            {
                OrganizationId = firstEntity.Id,
                CookbookId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
