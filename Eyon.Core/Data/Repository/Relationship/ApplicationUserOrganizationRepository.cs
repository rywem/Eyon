using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Core.Data.Repository
{
    public class ApplicationUserOrganizationRepository : Repository<ApplicationUserOrganization>, IApplicationUserOrganizationRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserOrganizationRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public ApplicationUserOrganization AddFromEntities( ApplicationUser firstEntity, Organization secondEntity )
        {
            ApplicationUserOrganization newObj = new ApplicationUserOrganization()
            {
                ApplicationUserId = firstEntity.Id,
                ObjectId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
