using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedTopicRepository : IRepository<FeedTopic>, IManyToManyRelationshipRepository<FeedTopic, Feed, Topic>
    {        
    }    
}
