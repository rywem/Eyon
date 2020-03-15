using Eyon.Models;
using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedRepository : IRepository<Feed>, IPrivacyRepository<Feed, ApplicationUserFeed>        
    {
        void UpdateFromIFeedItem( string currentUserId, Feed feed, IFeedItem entity);

        Feed AddFromIFeedItem( IFeedItem entity );
        Task<IEnumerable<Feed>> GetPublicFeedList( FeedSortBy sortBy, int take, int skip );
    }
}
