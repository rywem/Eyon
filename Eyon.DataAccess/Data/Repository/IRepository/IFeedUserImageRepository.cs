using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedUserImageRepository : IRepository<FeedUserImage>, IManyToManyRelationshipRepository<FeedUserImage, Feed, UserImage>
    {        
    }    
}
