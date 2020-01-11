using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IOrganizationRepository : IRepository<Organization>, IOwnerRepository<Organization, ApplicationUserOrganization>
    {        
        void Update(Organization organization);
        void UpdateIfOwner( string currentUserId, Organization organization );
    }    
}
