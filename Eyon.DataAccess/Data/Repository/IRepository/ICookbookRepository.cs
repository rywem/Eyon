using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IOrganizationRepository : IRepository<Organization>
    {        
        void Update(Organization organization);
    }    
}
