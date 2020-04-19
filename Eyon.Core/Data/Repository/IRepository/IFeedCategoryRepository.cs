using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedCategoryRepository : IRepository<FeedCategory>, IManyToManyRelationshipRepository<FeedCategory, Feed, Category>
    {        
    }    
}
