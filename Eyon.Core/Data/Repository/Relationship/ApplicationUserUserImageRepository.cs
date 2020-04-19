using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Core.Data.Repository
{
    public class ApplicationUserUserImageRepository : Repository<ApplicationUserUserImage>, IApplicationUserUserImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserUserImageRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public ApplicationUserUserImage AddFromEntities( ApplicationUser firstEntity, UserImage secondEntity )
        {
            var newObj = new ApplicationUserUserImage()
            {
                ApplicationUserId = firstEntity.Id,
                ObjectId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
