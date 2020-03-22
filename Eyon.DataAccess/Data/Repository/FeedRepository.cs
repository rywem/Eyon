﻿using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Enums;
using Eyon.Models.Errors;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using Eyon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository
{
    /// <summary>
    /// FeedRepository inherits from PrivacyRepository. 
    /// </summary>
    /// <remarks>
    /// Privacy information stored on the feed object is a denormalization 
    /// of data and is not the primary information location. Privacy 
    /// information is also stored on the base object's table. 
    /// </remarks>
    public class FeedRepository : PrivacyRepository<Feed, ApplicationUserFeed>, IFeedRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public override void Add( Feed entity )
        {
            base.Add(entity);
        }

        public Feed AddFromIFeedItem( IFeedItem entity )
        {
            var dateTimeNow = DateTime.UtcNow;
            Feed feed = new Feed()
            {
                Text = entity.Description,
                CreationDateTime = entity.CreationDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                Privacy = entity.Privacy
            };
            base.Add(feed);
            return feed;
        }

        public async Task<FeedViewModel> GetPublicFeedList( FeedSortBy sortBy = FeedSortBy.New, int skip = 0, int take = 100 )
        {

            FeedViewModel feedViewModel = new FeedViewModel();

            switch ( sortBy )
            {
                case FeedSortBy.New:
                    var query = await GetAllAsync(x => x.Privacy == Privacy.Public, r => r.OrderByDescending(x => x.CreationDateTime), includeProperties: "FeedTopic,FeedTopic.Topic", skip: skip, take: take);
                    feedViewModel.FeedItems = (from f in query
                                              select new FeedItemViewModel()
                                              {
                                                  CreationDateTime = f.CreationDateTime,
                                                  ModifiedDateTime = f.ModifiedDateTime,
                                                  Description = f.Text,
                                                  Privacy = f.Privacy,
                                                  Id = f.Id,
                                                  Topics = ( f.FeedTopic != null && f.FeedTopic.Count > 0 ) ? f.FeedTopic.Select(x => x.Topic).ToList() : new List<Topic>()
                                              }).ToList();

                    break;
                case FeedSortBy.Popular:
                case FeedSortBy.Random:
                default:
                    throw new NotImplementedException();
                    //break;
            }
            

            return feedViewModel;
        }

        public void Update( string currentUserId, Feed feed, IFeedItem entity )
        {
            var objFromDb = ( from r in _db.Feed
                              join a in _db.ApplicationUserFeed on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == feed.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                    throw new SafeException("An error ocurred.", new Exception(string.Format("Ownership relationship not found on record. currentUserId {0},  recipe.Id {1}", currentUserId, feed.Id)));

            feed.Privacy = entity.Privacy;
            objFromDb.Text = entity.Description;
            objFromDb.ModifiedDateTime = entity.ModifiedDateTime;
            dbSet.Update(objFromDb);
        }
    }
}
