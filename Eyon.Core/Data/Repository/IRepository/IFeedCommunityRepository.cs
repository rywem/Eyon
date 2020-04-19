using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedCommunityRepository : IRepository<FeedCommunity>, IManyToManyRelationshipRepository<FeedCommunity, Feed, Community>
    {        
    }    
}
