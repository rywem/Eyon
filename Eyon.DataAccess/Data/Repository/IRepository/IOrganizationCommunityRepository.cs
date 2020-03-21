using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IOrganizationCommunityRepository : IRepository<OrganizationCommunity>, IManyToManyRelationshipRepository<OrganizationCommunity, Organization, Community>
    {
    }
}
