using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IApplicationUserOrganizationRepository : IRepository<ApplicationUserOrganization>, IManyToManyRelationshipRepository<ApplicationUserOrganization, ApplicationUser, Organization>
    {


    }    
}
