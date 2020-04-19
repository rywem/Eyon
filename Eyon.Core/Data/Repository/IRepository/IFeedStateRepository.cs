using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedStateRepository : IRepository<FeedState>, IManyToManyRelationshipRepository<FeedState, Feed, State>
    {        
    }    
}
