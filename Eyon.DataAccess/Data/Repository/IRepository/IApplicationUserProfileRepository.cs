using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IApplicationUserProfileRepository : IRepository<ApplicationUserProfile>, IManyToManyRelationshipRepository<ApplicationUserProfile, ApplicationUser, Profile>
    {
    }
}
