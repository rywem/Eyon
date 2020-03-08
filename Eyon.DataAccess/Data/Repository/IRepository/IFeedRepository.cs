using Eyon.Models;
using Eyon.Models.Enums;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedRepository : IRepository<Feed>, IPrivacyRepository<Feed, ApplicationUserFeed>
    {
        void UpdateIfOwner( string currentUserId, Feed feed);

        
        Task<IEnumerable<Feed>> GetPublicFeedList( FeedSortBy sortBy, int take, int skip );
    }
}
