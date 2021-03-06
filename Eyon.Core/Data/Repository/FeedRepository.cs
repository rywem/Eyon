﻿using Eyon.Core.Data.Repository.IRepository;
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

namespace Eyon.Core.Data.Repository
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

        public Feed AddFromIFeedItem( IFeedItem entity )
        {
            var dateTimeNow = DateTime.UtcNow;
            Feed feed = new Feed()
            {
                Description = entity.Description,
                CreationDateTime = entity.CreationDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                Privacy = entity.Privacy
            };
            base.Add(feed);
            return feed;
        }

        public async Task<FeedViewModel> GetPublicFeedList( FeedSortBy sortBy = FeedSortBy.New, int skip = 0, int take = 0 )
        {

            FeedViewModel feedViewModel = new FeedViewModel();
            Func<IQueryable<Feed>, IQueryable<Feed>> additionalQueryable = null;
            
            if(skip > 0 && take > 0)
                additionalQueryable = x => x.Skip(skip).Take(take);
            else if ( skip > 0)
                 additionalQueryable = x => x.Skip(skip);            
            else if ( take > 0 )
                additionalQueryable += x => x.Take(take);

            switch ( sortBy )
            {
                case FeedSortBy.New:
                    var query = await GetAllAsync(x => x.Privacy == Privacy.Public, r => r.OrderByDescending(x => x.CreationDateTime), includeProperties: "FeedTopic,FeedTopic.Topic,FeedRecipe,FeedRecipe.Recipe,FeedRecipe.Recipe.RecipeUserImage.UserImage", additionalQueryable);
                    
                    feedViewModel.FeedItems = (from f in query
                                              select new FeedItemViewModel()
                                              {
                                                  FeedItem = f,
                                                  Topics = ( f.FeedTopic != null && f.FeedTopic.Count > 0 ) ? f.FeedTopic.Select(x => x.Topic).ToList() : new List<Topic>(),
                                                  Recipes = (f.FeedRecipe != null && f.FeedRecipe.Count > 0 ) ? f.FeedRecipe.Select(x => x.Recipe).ToList() : new List<Recipe>(),
                                                  UserImages = ( f.FeedRecipe != null && f.FeedRecipe.Count > 0 ) ? GetRecipeUserImages(f.FeedRecipe).ToList() : new List<UserImage>()
                                              } ).ToList();

                    break;
                case FeedSortBy.Popular:
                case FeedSortBy.Random:
                default:
                    throw new NotImplementedException();
                    //break;
            }
            

            return feedViewModel;
        }

        private IEnumerable<UserImage> GetRecipeUserImages(ICollection<FeedRecipe> feedRecipes )
        {
            foreach ( var feedRecipe in feedRecipes )
            {
                foreach ( var recipeUserImage in feedRecipe.Recipe.RecipeUserImage )
                {
                    yield return recipeUserImage.UserImage;
                }
            }
        }

        public void Update( Feed feed, IFeedItem entity )
        {
            var objFromDb = _db.Feed.FirstOrDefault(s => s.Id == feed.Id);
            if ( objFromDb == null )
                throw new SafeException("An error ocurred.", new Exception(string.Format("Object not found Feed.Update() Feed ID: {0}", feed.Id)));

            feed.Privacy = entity.Privacy;
            objFromDb.Privacy = entity.Privacy;
            objFromDb.Name = entity.Name;
            objFromDb.Description = entity.Description;
            objFromDb.ModifiedDateTime = entity.ModifiedDateTime;
            dbSet.Update(objFromDb);
        }


        public void UpdateIfOwner( string currentUserId, Feed feed, IFeedItem entity )
        {
            var objFromDb = ( from r in _db.Feed
                              join a in _db.ApplicationUserFeed on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == feed.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                    throw new SafeException("An error ocurred.", new Exception(string.Format("Ownership relationship not found on record. currentUserId {0},  recipe.Id {1}", currentUserId, feed.Id)));

            feed.Privacy = entity.Privacy;
            objFromDb.Privacy = entity.Privacy;
            objFromDb.Name = entity.Name;
            objFromDb.Description = entity.Description;
            objFromDb.ModifiedDateTime = entity.ModifiedDateTime;
            dbSet.Update(objFromDb);
        }
    }
}
