using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IOrganizationCookbookRepository : IRepository<OrganizationCookbook>, IManyToManyRelationshipRepository<OrganizationCookbook, Organization, Cookbook>
    {
    }
}
