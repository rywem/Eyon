using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICommunityCookbookRepository : IRepository<CommunityCookbook>, IManyToManyRelationshipRepository<CommunityCookbook, Community, Cookbook>
    {        
    }    
}
