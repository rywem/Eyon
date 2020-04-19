using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IOrganizationCommunityRepository : IRepository<OrganizationCommunity>, IManyToManyRelationshipRepository<OrganizationCommunity, Organization, Community>
    {
    }
}
