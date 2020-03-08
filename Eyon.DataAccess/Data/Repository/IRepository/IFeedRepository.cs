using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedRepository : IRepository<Feed>, IPrivacyRepository<Feed, ApplicationUserFeed>
    {
        void UpdateIfOwner( string currentUserId, Feed feed);
    }
}
