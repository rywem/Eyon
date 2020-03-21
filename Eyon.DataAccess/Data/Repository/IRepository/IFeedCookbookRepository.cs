using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedCookbookRepository : IRepository<FeedCookbook>, IManyToManyRelationshipRepository<FeedCookbook, Feed, Cookbook>
    {        
    }    
}
