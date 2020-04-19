using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Core.Data.Repository
{
    public class ApplicationUserProfileRepository : Repository<ApplicationUserProfile>, IApplicationUserProfileRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserProfileRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
        public ApplicationUserProfile AddFromEntities( ApplicationUser firstEntity, Profile secondEntity )
        {
            var newObj = new ApplicationUserProfile()
            {
                ApplicationUserId = firstEntity.Id,
                ObjectId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
