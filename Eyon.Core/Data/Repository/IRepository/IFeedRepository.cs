﻿using Eyon.Models;
using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedRepository : IRepository<Feed>, IPrivacyRepository<Feed, ApplicationUserFeed>        
    {
        void Update( Feed feed, IFeedItem entity);
        void UpdateIfOwner( string currentUserId, Feed feed, IFeedItem entity );

        Feed AddFromIFeedItem( IFeedItem entity );
        Task<FeedViewModel> GetPublicFeedList( FeedSortBy sortBy, int take, int skip );
    }
}
