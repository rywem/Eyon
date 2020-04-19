using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedProfileRepository : IRepository<FeedProfile>, IManyToManyRelationshipRepository<FeedProfile, Feed, Profile>
    {        
    }    
}
