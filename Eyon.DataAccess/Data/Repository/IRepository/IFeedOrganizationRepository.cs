using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedOrganizationRepository : IRepository<FeedOrganization>, IManyToManyRelationshipRepository<FeedOrganization, Feed, Organization>
    {        
    }    
}
