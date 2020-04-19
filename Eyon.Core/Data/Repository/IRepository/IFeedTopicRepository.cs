using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedTopicRepository : IRepository<FeedTopic>, IManyToManyRelationshipRepository<FeedTopic, Feed, Topic>
    {        
    }    
}
