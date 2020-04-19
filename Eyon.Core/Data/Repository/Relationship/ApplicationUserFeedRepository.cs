using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Core.Data.Repository
{
    public class ApplicationUserFeedRepository : Repository<ApplicationUserFeed>, IApplicationUserFeedRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserFeedRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public ApplicationUserFeed AddFromEntities( ApplicationUser firstEntity, Feed secondEntity )
        {
            ApplicationUserFeed newObj = new ApplicationUserFeed()
            {
                ApplicationUserId = firstEntity.Id,
                ObjectId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
