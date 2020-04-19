using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IOrganizationRepository : IRepository<Organization>, IOwnerRepository<Organization, ApplicationUserOrganization>
    {        
        void Update(Organization organization);
        void UpdateIfOwner( string currentUserId, Organization organization );
    }    
}
