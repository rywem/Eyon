using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IOrganizationCookbookRepository : IRepository<OrganizationCookbook>, IManyToManyRelationshipRepository<OrganizationCookbook, Organization, Cookbook>
    {
    }
}
