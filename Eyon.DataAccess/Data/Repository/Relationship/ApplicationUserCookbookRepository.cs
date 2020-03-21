using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class ApplicationUserCookbookRepository : Repository<ApplicationUserCookbook>, IApplicationUserCookbookRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserCookbookRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public ApplicationUserCookbook AddFromEntities( ApplicationUser firstEntity, Cookbook secondEntity )
        {
            ApplicationUserCookbook newObj = new ApplicationUserCookbook()
            {
                ApplicationUserId = firstEntity.Id,
                ObjectId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
