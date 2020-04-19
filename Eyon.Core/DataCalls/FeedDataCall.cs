using Eyon.Core.Data.Repository.IRepository;
using Eyon.Core.DataCalls.IDataCall;
using Eyon.Models;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.Core.DataCalls
{
    public class FeedDataCall : IFeedDataCall
    {
        private readonly IUnitOfWork _unitOfWork;
        public FeedDataCall( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }

        #region Add


        public async Task<Feed> AddFeedWithRelationship( string currentApplicationUserId, IFeedItem feedItem, bool saveOnRelationshipInsert = true )
        {
            var feed = _unitOfWork.Feed.AddFromIFeedItem(feedItem);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Feed.AddOwnerRelationship(currentApplicationUserId, feed, new ApplicationUserFeed());
            
            if ( saveOnRelationshipInsert == true )
                await _unitOfWork.SaveAsync();

            return feed;

        }
        //private Feed AddFeed(IFeedItem item )
        //{
        //    return _unitOfWork.Feed.AddFromIFeedItem(item);
        //}

        //private void AddOwnerRelationship(string currentApplicationUserId, Feed feed )
        //{
        //    _unitOfWork.Feed.AddOwnerRelationship(currentApplicationUserId, feed, new ApplicationUserFeed());
        //}

        //public FeedTopic AddFeedTopic(Feed feed, Topic topic)
        //{
        //    return _unitOfWork.FeedTopic.AddFromEntities(feed, topic);
        //}

        //public FeedRecipe AddFeedRecipe(Feed feed, Recipe recipe )
        //{            
        //    return _unitOfWork.FeedRecipe.AddFromEntities(feed, recipe );
        //}

        /// <summary>
        /// Adds the FeedCommunity, FeedState, and FeedCountry relationships
        /// </summary>
        /// <param name="feed">A Feed object</param>
        /// <param name="community">A Community object</param>
        public void AddFeedCommunityStateCountryRelationships(Feed feed, Community community )
        {
            var communityFromDb = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == community.Id, includeProperties: "Country,CommunityState,CommunityState.State");

            if ( communityFromDb != null )
            {
                _unitOfWork.FeedCommunity.AddFromEntities(feed, communityFromDb);

                if ( communityFromDb.Country != null )
                {
                    _unitOfWork.FeedCountry.AddFromEntities(feed, communityFromDb.Country);
                }
                if ( communityFromDb.CommunityState != null && communityFromDb.CommunityState.State != null )
                {
                    _unitOfWork.FeedState.AddFromEntities(feed, communityFromDb.CommunityState.State);
                }
            }
        }

        /// <summary>
        /// Async adds the FeedCommunity, FeedState, and FeedCountry relationships
        /// </summary>
        /// <param name="feed">A Feed object</param>
        /// <param name="community">A Community object</param>
        /// <returns>A task</returns>
        public async Task AddFeedCommunityStateCountryRelationshipsAsync( Feed feed, Community community )
        {
            var communityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == community.Id, includeProperties: "Country,CommunityState,CommunityState.State");

            if ( communityFromDb != null )
            {
                _unitOfWork.FeedCommunity.AddFromEntities(feed, communityFromDb);

                if ( communityFromDb.Country != null )
                {
                    _unitOfWork.FeedCountry.AddFromEntities(feed, communityFromDb.Country);
                }
                if ( communityFromDb.CommunityState != null && communityFromDb.CommunityState.State != null )
                {
                    _unitOfWork.FeedState.AddFromEntities(feed, communityFromDb.CommunityState.State);
                }
            }
        }

        public FeedOrganization AddFeedOrganization( Feed feed, Organization organization )
        {
            return _unitOfWork.FeedOrganization.AddFromEntities(feed, organization);
        }
        public FeedCommunity AddFeedCommunity( Feed feed, Community community )
        {
            return _unitOfWork.FeedCommunity.AddFromEntities(feed, community);
        }

        public FeedState AddFeedState( Feed feed, State state )
        {
            return _unitOfWork.FeedState.AddFromEntities(feed, state);
        }

        public FeedCountry AddFeedCountry( Feed feed, Country country )
        {
            return _unitOfWork.FeedCountry.AddFromEntities(feed, country);
        }

        public FeedCookbook AddFeedCookbook( Feed feed, Cookbook cookbook )
        {
            return _unitOfWork.FeedCookbook.AddFromEntities(feed, cookbook);
        }

        public FeedCategory AddFeedCategory( Feed feed, Category category )
        {
            return _unitOfWork.FeedCategory.AddFromEntities(feed, category);
        }
        public FeedUserImage AddFeedUserImage( Feed feed, UserImage userImage )
        {
            return _unitOfWork.FeedUserImage.AddFromEntities(feed, userImage);
        }
        public FeedTopic AddFeedTopic(Feed feed, Topic topic)
        {
            return _unitOfWork.FeedTopic.AddFromEntities(feed, topic);
        }

        public FeedProfile AddFeedProfile( Feed feed, Profile profile )
        {
            return _unitOfWork.FeedProfile.AddFromEntities(feed, profile);
        }


        public FeedRecipe AddFeedRecipe( Feed feed, Recipe recipe)
        {
            return _unitOfWork.FeedRecipe.AddFromEntities(feed, recipe);
        }

        #endregion

        #region Update
        public Feed UpdateFeed(string currentApplicationUserId, Feed feed, IFeedItem item )
        {
            _unitOfWork.Feed.UpdateIfOwner(currentApplicationUserId, feed, item);
            return feed;
        }
        #endregion

        #region Remove


        public void RemoveFeedCategory( Feed feed, Category category )
        {
            var feedCategory = _unitOfWork.FeedCategory.GetFirstOrDefault(x => x.FeedId == feed.Id && x.CategoryId == category.Id);
            if( feedCategory != null )
                RemoveFeedCategory(feedCategory);
        }
        public void RemoveFeedCategory( FeedCategory feedCategory )
        {
            _unitOfWork.FeedCategory.Remove(feedCategory);
        }

        public void RemoveFeedCommunity( Feed feed, Community community )
        {
            var feedCommunity = _unitOfWork.FeedCommunity.GetFirstOrDefault(x => x.FeedId == feed.Id && x.CommunityId == community.Id);
            if ( feedCommunity != null )
                RemoveFeedCommunity(feedCommunity);
        }

        public async Task RemoveFeedCommunityAsync( Feed feed, Community community )
        {
            var feedCommunity = await _unitOfWork.FeedCommunity.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.CommunityId == community.Id);
            if (feedCommunity != null )
                RemoveFeedCommunity(feedCommunity);
        }

        public void RemoveFeedCommunity(FeedCommunity feedCommunity)
        {
            _unitOfWork.FeedCommunity.Remove(feedCommunity);
        }

        public void RemoveFeedCommunityStateCountryRelationships(Feed feed, Community community)
        {
            var communityFromDb = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == community.Id, includeProperties: "CommunityState,CommunityState.State,Country");
            if ( communityFromDb != null )
            {
                RemoveFeedCommunity(feed, communityFromDb);
                if ( communityFromDb.Country != null )
                {
                    RemoveFeedCountry(feed, communityFromDb.Country);
                }
                if ( communityFromDb.CommunityState != null && communityFromDb.CommunityState.State != null )
                {
                    RemoveFeedState(feed, communityFromDb.CommunityState.State);

                }
            }
        }

        public async Task RemoveFeedCommunityStateCountryRelationshipsAsync( Feed feed, Community community )
        {
            var communityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == community.Id, includeProperties: "CommunityState,CommunityState.State,Country");
            if ( communityFromDb != null )
            {
                await RemoveFeedCommunityAsync(feed, communityFromDb);
                if ( communityFromDb.Country != null )
                {
                    await RemoveFeedCountryAsync(feed, communityFromDb.Country);
                }
                if ( communityFromDb.CommunityState != null && communityFromDb.CommunityState.State != null )
                {
                    await RemoveFeedStateAsync(feed, communityFromDb.CommunityState.State);
                }
            }
        }

        public void RemoveFeedCookbook( Feed feed, Cookbook cookbook )
        {
            var feedCookbook = _unitOfWork.FeedCookbook.GetFirstOrDefault(x => x.FeedId == feed.Id && x.CookbookId == cookbook.Id);
            if ( feedCookbook != null )
                RemoveFeedCookbook(feedCookbook);
        }
        public async Task RemoveFeedCookbookAsync( Feed feed, Cookbook cookbook )
        {
            var feedCookbook = await _unitOfWork.FeedCookbook.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.CookbookId == cookbook.Id);
            
            if ( feedCookbook != null )
            RemoveFeedCookbook(feedCookbook);
        }

        public void RemoveFeedCookbook(FeedCookbook feedCookbook )
        {
            _unitOfWork.FeedCookbook.Remove(feedCookbook);
        }

        public void RemoveFeedState(Feed feed, State state )
        {
            var feedState = _unitOfWork.FeedState.GetFirstOrDefault(x => x.FeedId == feed.Id && x.StateId == state.Id);
            
            if ( feedState != null )
                RemoveFeedState(feedState);
        }

        public async Task RemoveFeedStateAsync( Feed feed, State state )
        {
            var feedState = await _unitOfWork.FeedState.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.StateId == state.Id);
            if(feedState != null )
                RemoveFeedState(feedState);
        }

        public void RemoveFeedState( FeedState feedState )
        {
            _unitOfWork.FeedState.Remove(feedState);
        }

        public void RemoveFeedTopic( FeedTopic feedTopic )
        {
            _unitOfWork.FeedTopic.Remove(feedTopic);
        }

        public void RemoveFeedTopic( Feed feed, Topic topic )
        {
            var feedTopic = _unitOfWork.FeedTopic.GetFirstOrDefault(x => x.FeedId == feed.Id && x.TopicId == topic.Id);
            
            if(feedTopic != null )
                RemoveFeedTopic(feedTopic);
        }

        public async Task RemoveFeedTopicAsync( Feed feed, Topic topic )
        {
            var feedTopic = await _unitOfWork.FeedTopic.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.TopicId == topic.Id);
            if ( feedTopic != null )
                RemoveFeedTopic(feedTopic);
        }


        public void RemoveFeedRecipe( FeedRecipe feedRecipe )
        {
            _unitOfWork.FeedRecipe.Remove(feedRecipe);
        }

        public void RemoveFeedRecipe( Feed feed, Recipe recipe )
        {
            var feedRecipe = _unitOfWork.FeedRecipe.GetFirstOrDefault(x => x.FeedId == feed.Id && x.RecipeId == recipe.Id);
            if ( feedRecipe != null )
                RemoveFeedRecipe(feedRecipe);
        }

        public async Task RemoveFeedRecipeAsync( Feed feed, Recipe recipe)
        {
            var feedRecipe = await _unitOfWork.FeedRecipe.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.RecipeId == recipe.Id);
            if ( feedRecipe != null )
                RemoveFeedRecipe(feedRecipe);
        }

        public void RemoveFeedCountry(Feed feed, Country country )
        {
            var feedCountry = _unitOfWork.FeedCountry.GetFirstOrDefault(x => x.FeedId == feed.Id && x.CountryId == country.Id);
            if ( feedCountry != null )
                RemoveFeedCountry(feedCountry);
        }

        public async Task RemoveFeedCountryAsync( Feed feed, Country country )
        {
            var feedCountry = await _unitOfWork.FeedCountry.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.CountryId == country.Id);
            if ( feedCountry != null )
                RemoveFeedCountry(feedCountry);
        }
        public void RemoveFeedCountry(FeedCountry feedCountry )
        {
            _unitOfWork.FeedCountry.Remove(feedCountry);
        }

        public void RemoveFeedOrganization( FeedOrganization feedOrganization )
        {
            _unitOfWork.FeedOrganization.Add(feedOrganization);
        }

        public void RemoveFeedOrganization( Feed feed, Organization organization )
        {
            var feedOrganization = _unitOfWork.FeedOrganization.GetFirstOrDefault(x => x.FeedId == feed.Id && x.OrganizationId == organization.Id);
            if ( feedOrganization != null )
                RemoveFeedOrganization(feedOrganization);
        }

        public async Task RemoveFeedOrganizationAsync( Feed feed, Organization organization )
        {
            var feedOrganization = await _unitOfWork.FeedOrganization.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.OrganizationId == organization.Id);
            if ( feedOrganization != null )
                RemoveFeedOrganization(feedOrganization);
        }

        public void RemoveFeedProfile( FeedProfile feedProfile )
        {
            _unitOfWork.FeedProfile.Add(feedProfile);
        }

        public void RemoveFeedProfile( Feed feed, Profile profile )
        {
            var feedProfile = _unitOfWork.FeedProfile.GetFirstOrDefault(x => x.FeedId == feed.Id && x.ProfileId == profile.Id);
            if ( feedProfile != null )
                RemoveFeedProfile(feedProfile);
        }

        public async Task RemoveFeedProfileAsync( Feed feed, Profile profile )
        {
            var feedProfile = await _unitOfWork.FeedProfile.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.ProfileId == profile.Id);
            if ( feedProfile != null )
                RemoveFeedProfile(feedProfile);
        }

        public void RemoveFeedUserImage( FeedUserImage feedUserImage )
        {
            _unitOfWork.FeedUserImage.Add(feedUserImage);
        }

        public void RemoveFeedUserImage( Feed feed, UserImage userImage )
        {
            var feedUserImage = _unitOfWork.FeedUserImage.GetFirstOrDefault(x => x.FeedId == feed.Id && x.UserImageId == userImage.Id);
            if ( feedUserImage != null )
                RemoveFeedUserImage(feedUserImage);
        }

        public async Task RemoveFeedUserImageAsync( Feed feed, UserImage userImage )
        {
            var feedUserImage = await _unitOfWork.FeedUserImage.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.UserImageId == userImage.Id);
            if ( feedUserImage != null )
                RemoveFeedUserImage(feedUserImage);
        }

        #endregion
    }
}
