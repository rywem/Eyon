using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedOrganizationRepository : IRepository<FeedOrganization>, IManyToManyRelationshipRepository<FeedOrganization, Feed, Organization>
    {        
    }    
}
