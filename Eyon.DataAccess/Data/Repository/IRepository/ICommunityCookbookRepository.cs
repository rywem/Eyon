using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICommunityCookbookRepository : IRepository<CommunityCookbook>, IManyToManyRelationshipRepository<CommunityCookbook, Community, Cookbook>
    {        
    }    
}
