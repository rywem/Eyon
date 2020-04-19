using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Eyon.Models.Enums;
using Eyon.Core.Orchestrators.IOrchestrator;
using Eyon.Core.DataCalls.IDataCall;

namespace Eyon.Core.Orchestrators
{
    public class FeedOrchestrator : IFeedOrchestrator
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedDataCall _feedDataCall;
        public FeedOrchestrator( IUnitOfWork unitOfWork, IFeedDataCall feedDataCall )
        {
            this._unitOfWork = unitOfWork;
            this._feedDataCall = feedDataCall;
        }
        public async Task<FeedViewModel> GetPublicFeedViewModel( FeedSortBy sortBy = FeedSortBy.New, int skip = 0, int take = 0 )
        {
            return await _unitOfWork.Feed.GetPublicFeedList(sortBy, skip, take );
        }


        public async Task AddTransactionAsync( string currentApplicationUserId, FeedItemViewModel feedViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await AddAsync(currentApplicationUserId, feedViewModel);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task AddAsync( string currentApplicationUserId, FeedItemViewModel feedViewModel )
        {
            var feed = await _feedDataCall.AddFeedWithRelationship(currentApplicationUserId, feedViewModel.FeedItem, false);

            if ( feedViewModel.Categories != null && feedViewModel.Categories.Count > 0 )
            {
                foreach ( var item in feedViewModel.Categories )
                {
                    _feedDataCall.AddFeedCategory(feed, item);
                }
            }

            if ( feedViewModel.Communities != null && feedViewModel.Communities.Count > 0 )
            {
                foreach ( var item in feedViewModel.Communities )
                {
                    await _feedDataCall.AddFeedCommunityStateCountryRelationshipsAsync(feed, item);
                }
            }

            if ( feedViewModel.Cookbooks != null && feedViewModel.Cookbooks.Count > 0 )
            {
                foreach ( var item in feedViewModel.Cookbooks )
                {
                    _feedDataCall.AddFeedCookbook(feed, item);
                }
            }

            if ( feedViewModel.Organizations != null && feedViewModel.Organizations.Count > 0 )
            {
                foreach ( var item in feedViewModel.Organizations )
                {
                    _feedDataCall.AddFeedOrganization(feed, item);
                }
            }
            if ( feedViewModel.Profiles != null && feedViewModel.Profiles.Count > 0 )
            {
                foreach ( var item in feedViewModel.Profiles )
                {
                    _feedDataCall.AddFeedProfile(feed, item);
                }
            }

            if ( feedViewModel.Recipes != null && feedViewModel.Recipes.Count > 0 )
            {
                foreach ( var item in feedViewModel.Recipes )
                {
                    _feedDataCall.AddFeedRecipe(feed, item);
                }
            }

            if ( feedViewModel.Topics != null && feedViewModel.Topics.Count > 0 )
            {
                foreach ( var item in feedViewModel.Topics )
                {
                    _feedDataCall.AddFeedTopic(feed, item);
                }
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync( Feed feed )
        {

            if ( feed.FeedCommunity != null )
            {
                foreach ( var item in feed.FeedCommunity )
                {
                    _unitOfWork.FeedCommunity.Remove(item);
                }
            }

            if ( feed.FeedState != null )
            {
                foreach ( var item in feed.FeedState )
                {
                    _unitOfWork.FeedState.Remove(item);
                }
            }

            if ( feed.FeedOrganization != null )
            {
                foreach ( var item in feed.FeedOrganization )
                {
                    _unitOfWork.FeedOrganization.Remove(item);
                }
            }

            if ( feed.FeedCategory != null )
            {
                foreach ( var item in feed.FeedCategory )
                {
                    _unitOfWork.FeedCategory.Remove(item);
                }
            }

            if ( feed.FeedCountry != null )
            {
                foreach ( var item in feed.FeedCountry )
                {
                    _unitOfWork.FeedCountry.Remove(item);
                }
            }

            if ( feed.FeedCookbook != null )
            {
                foreach ( var item in feed.FeedCookbook )
                {
                    _unitOfWork.FeedCookbook.Remove(item);
                }
            }

            if ( feed.FeedRecipe != null )
            {
                foreach ( var item in feed.FeedRecipe )
                {
                    _unitOfWork.FeedRecipe.Remove(item);
                }
            }

            if ( feed.FeedProfile != null )
            {
                foreach ( var item in feed.FeedProfile )
                {
                    _unitOfWork.FeedProfile.Remove(item);
                }
            }

            if ( feed.FeedTopic != null )
            {
                foreach ( var item in feed.FeedTopic )
                {
                    _unitOfWork.FeedTopic.Remove(item);
                }
            }

            if ( feed.ApplicationUserOwner != null )
            {
                foreach ( var item in feed.ApplicationUserOwner )
                {
                    _unitOfWork.ApplicationUserFeed.Remove(item);
                }
            }
            _unitOfWork.Feed.Remove(feed);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTransactionAsync( Feed feed )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await DeleteAsync(feed);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task UpdateAsync( string currentApplicationUserId, FeedItemViewModel feedItemViewModel )
        {
            _feedDataCall.UpdateFeed(currentApplicationUserId, feedItemViewModel.Feed, feedItemViewModel.FeedItem);
            await _unitOfWork.SaveAsync();

            var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id,
                includeProperties: "FeedCommunity,FeedCommunity.Community,FeedOrganization,FeedOrganization.Organization," +
                "FeedCategory,FeedCategory.Category,FeedCookbook,FeedCookbook.Cookbook,FeedRecipe,FeedRecipe.Recipe," +
                "FeedUserImage,FeedUserImage.UserImage,FeedProfile,FeedProfile.Profile,FeedTopic,FeedTopic.Topic");

            // Add new Community 
            if ( feedItemViewModel.Communities != null && feedItemViewModel.Communities.Count > 0 )
            {
                foreach ( var item in feedItemViewModel.Communities )
                {
                    // If it already exists, we don't need to add it again, so continue
                    if ( !feedFromDb.FeedCommunity.Any(x => x.CommunityId == item.Id) )
                    {
                        var communityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                        if ( communityFromDb != null )
                            await _feedDataCall.AddFeedCommunityStateCountryRelationshipsAsync(feedFromDb, communityFromDb);
                    }
                }
            }
            // Remove communities that are no longer part of this feed
            if ( feedFromDb.FeedCommunity != null && feedFromDb.FeedCommunity.Count > 0 )
            {
                foreach ( var item in feedFromDb.FeedCommunity )
                {
                    if ( !feedItemViewModel.Communities.Any(x => x.Id == item.CommunityId) )
                    {
                        _feedDataCall.RemoveFeedCommunityStateCountryRelationships(feedFromDb, item.Community);
                    }
                }
            }
            await _unitOfWork.SaveAsync();

            // Add organizations
            if ( feedItemViewModel.Organizations != null && feedItemViewModel.Organizations.Count > 0 )
            {
                foreach ( var item in feedItemViewModel.Organizations )
                {
                    if ( !feedFromDb.FeedOrganization.Any(x => x.OrganizationId == item.Id) )
                    {
                        var orgFromDb = await _unitOfWork.Organization.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                        if ( orgFromDb != null )
                            _feedDataCall.AddFeedOrganization(feedFromDb, orgFromDb);
                    }
                }
            }
            // Remove Organizations
            if ( feedFromDb.FeedOrganization != null && feedFromDb.FeedOrganization.Count > 0 )
            {
                foreach ( var item in feedFromDb.FeedOrganization )
                {
                    if ( !feedItemViewModel.Organizations.Any(x => x.Id == item.OrganizationId) )
                        await _feedDataCall.RemoveFeedOrganizationAsync(feedFromDb, item.Organization);
                }
            }

            // Add FeedCategory
            if ( feedItemViewModel.Categories != null && feedItemViewModel.Categories.Count > 0 )
            {
                foreach ( var item in feedItemViewModel.Categories )
                {
                    if ( !feedFromDb.FeedCategory.Any(x => x.CategoryId == item.Id))
                    {
                        var categoryFromDb = await _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                        if ( categoryFromDb != null )
                            _feedDataCall.AddFeedCategory(feedFromDb, categoryFromDb);
                    }
                }
            }
            // Remove FeedCategory
            if ( feedFromDb.FeedCategory != null && feedFromDb.FeedCategory.Count > 0 )
            {
                foreach ( var item in feedFromDb.FeedCategory )
                {
                    if ( !feedItemViewModel.Categories.Any(x => x.Id == item.CategoryId) )
                        _feedDataCall.RemoveFeedCategory(feedFromDb, item.Category);
                }
            }
            
            // Add Cookbooks
            if ( feedItemViewModel.Cookbooks != null && feedItemViewModel.Cookbooks.Count > 0 )
            {
                foreach ( var item in feedItemViewModel.Cookbooks )
                {
                    if (!feedFromDb.FeedCookbook.Any(x => x.CookbookId == item.Id))
                    {
                        var cookbookFromDb = await _unitOfWork.Cookbook.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                        if ( cookbookFromDb != null )
                            _feedDataCall.AddFeedCookbook(feedFromDb, cookbookFromDb);
                    }
                }
            }
            // Remove Cookbooks
            if ( feedFromDb.FeedCookbook != null && feedFromDb.FeedCookbook.Count > 0 )
            {
                foreach ( var item in feedFromDb.FeedCookbook )
                {
                    if ( !feedItemViewModel.Cookbooks.Any(x => x.Id == item.CookbookId) )
                        await _feedDataCall.RemoveFeedCookbookAsync(feedFromDb, item.Cookbook);
                }
            }
            // Add Recipes
            if ( feedItemViewModel.Recipes != null && feedItemViewModel.Recipes.Count > 0 )
            {
                foreach ( var item in feedItemViewModel.Recipes )
                {
                    if ( !feedFromDb.FeedRecipe.Any(x => x.RecipeId == item.Id) )
                    {
                        var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                        if ( recipeFromDb != null )
                            _feedDataCall.AddFeedRecipe(feedFromDb, recipeFromDb);
                    }
                }
            }
            // Remove Recipes
            if ( feedFromDb.FeedRecipe != null && feedFromDb.FeedRecipe.Count > 0 )
            {
                foreach ( var item in feedFromDb.FeedRecipe )
                {
                    if ( !feedItemViewModel.Recipes.Any(x => x.Id == item.RecipeId) )
                        await _feedDataCall.RemoveFeedRecipeAsync(feedFromDb, item.Recipe);
                }
            }

            
            // Add Profiles
            if ( feedItemViewModel.Profiles != null && feedItemViewModel.Profiles.Count > 0 )
            {
                foreach ( var item in feedItemViewModel.Profiles )
                {
                    if ( !feedFromDb.FeedProfile.Any(x => x.ProfileId == item.Id) )
                    {
                        var profileFromDb = await _unitOfWork.Profile.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                        if ( profileFromDb != null )
                            _feedDataCall.AddFeedProfile(feedFromDb, profileFromDb);
                    }
                }
            }

            // Remove Profiles
            if ( feedFromDb.FeedProfile != null && feedFromDb.FeedProfile.Count > 0 )
            {
                foreach ( var item in feedFromDb.FeedProfile )
                {
                    if ( !feedItemViewModel.Profiles.Any(x => x.Id == item.ProfileId) )
                        await _feedDataCall.RemoveFeedProfileAsync(feedFromDb, item.Profile);
                }
            }
            
            // Add Topics
            if ( feedItemViewModel.Topics != null && feedItemViewModel.Topics.Count > 0 )
            {
                foreach ( var item in feedItemViewModel.Topics )
                {
                    if ( !feedFromDb.FeedTopic.Any(x => x.TopicId == item.Id) )
                    {
                        var topicFromDb = await _unitOfWork.Topic.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                        if ( topicFromDb != null )
                            _feedDataCall.AddFeedTopic(feedFromDb, topicFromDb);
                    }
                }
            }
            // Remove Topics
            if ( feedFromDb.FeedTopic != null && feedFromDb.FeedTopic.Count > 0 )
            {
                foreach ( var item in feedFromDb.FeedTopic )
                {
                    if ( !feedItemViewModel.Topics.Any(x => x.Id == item.TopicId) )
                        await _feedDataCall.RemoveFeedTopicAsync(feedFromDb, item.Topic);
                }
            }

            // Add Images
            if ( feedItemViewModel.UserImages != null && feedItemViewModel.UserImages.Count > 0 )
            {
                foreach ( var item in feedItemViewModel.UserImages )
                {
                    if ( !feedFromDb.FeedUserImage.Any(x => x.UserImageId == item.Id) )
                    {
                        var UserImageFromDb = await _unitOfWork.UserImage.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                        if ( UserImageFromDb != null )
                            _feedDataCall.AddFeedUserImage(feedFromDb, UserImageFromDb);
                    }
                }
            }
            // Remove Images
            if ( feedFromDb.FeedUserImage != null && feedFromDb.FeedUserImage.Count > 0 )
            {
                foreach ( var item in feedFromDb.FeedUserImage )
                {
                    if ( !feedItemViewModel.UserImages.Any(x => x.Id == item.UserImageId) )
                        await _feedDataCall.RemoveFeedUserImageAsync(feedFromDb, item.UserImage);
                }
            }
            
        }

        public async Task UpdateTransactionAsync( string currentApplicationUserId, FeedItemViewModel feedItemViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await UpdateAsync(currentApplicationUserId, feedItemViewModel);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
