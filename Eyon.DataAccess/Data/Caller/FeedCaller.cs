﻿using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Caller
{
    public class FeedCaller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FeedCaller( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }

        #region Add

        public Feed AddFeed(IFeedItem item )
        {
            return _unitOfWork.Feed.AddFromIFeedItem(item);
        }

        public void AddOwnerRelationship(string currentApplicationUserId, Feed feed )
        {
            _unitOfWork.Feed.AddOwnerRelationship(currentApplicationUserId, feed, new ApplicationUserFeed());
        }

        public FeedTopic AddFeedTopic(Feed feed, Topic topic)
        {
            var feedTopic = new FeedTopic() { FeedId = feed.Id, TopicId = topic.Id };
            _unitOfWork.FeedTopic.Add(feedTopic);
            return feedTopic;
        }

        public FeedRecipe AddFeedRecipe(Feed feed, Recipe recipe )
        {
            var feedRecipe = new FeedRecipe() { FeedId = feed.Id, RecipeId = recipe.Id };
            _unitOfWork.FeedRecipe.Add(feedRecipe );
            return feedRecipe;
        }

        /// <summary>
        /// Adds the FeedCommunity, FeedState, and FeedCountry relationships
        /// </summary>
        /// <param name="feed">A Feed object</param>
        /// <param name="community">A Community object</param>
        public void AddFeedCommunityStateCountryRelationships(Feed feed, Community community )
        {
            var communityFromDb = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == community.Id, includeProperties: "CommunityState,CommunityState.State,CommunityState.State.Country");

            if ( communityFromDb != null )
            {
                AddFeedCommunity(feed, communityFromDb);
                if ( communityFromDb.CommunityState != null && communityFromDb.CommunityState.State != null)
                {
                    AddFeedState(feed, communityFromDb.CommunityState.State);

                    if ( communityFromDb.CommunityState.State.Country != null )
                        AddFeedCountry(feed, communityFromDb.CommunityState.State.Country);
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
            var communityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == community.Id, includeProperties: "CommunityState,CommunityState.State,CommunityState.State.Country");

            if ( communityFromDb != null )
            {
                AddFeedCommunity(feed, communityFromDb);
                if ( communityFromDb.CommunityState != null && communityFromDb.CommunityState.State != null )
                {
                    AddFeedState(feed, communityFromDb.CommunityState.State);

                    if ( communityFromDb.CommunityState.State.Country != null )
                        AddFeedCountry(feed, communityFromDb.CommunityState.State.Country);
                }
            }
        }

        public FeedCommunity AddFeedCommunity(Feed feed, Community community)
        {
            var feedCommunity = new FeedCommunity() { CommunityId = community.Id, FeedId = feed.Id };
            _unitOfWork.FeedCommunity.Add(feedCommunity);
            return feedCommunity;
        }

        public FeedState AddFeedState(Feed feed, State state)
        {
            var feedState = new FeedState() { FeedId = feed.Id, StateId = state.Id };
            _unitOfWork.FeedState.Add(feedState);
            return feedState;
        }

        public FeedCountry AddFeedCountry(Feed feed, Country country )
        {
            var feedCountry = new FeedCountry() { FeedId = feed.Id, CountryId = country.Id };
            _unitOfWork.FeedCountry.Add(feedCountry);
            return feedCountry;
        }

        public FeedCookbook AddFeedCookbook(Feed feed, Cookbook cookbook )
        {
            var feedCookbook = new FeedCookbook()
            {
                CookbookId = cookbook.Id,
                FeedId = feed.Id
            };
            _unitOfWork.FeedCookbook.Add(feedCookbook);
            return feedCookbook;
        }
        #endregion

        #region Update
        public Feed UpdateFeed(string currentApplicationUserId, Feed feed, IFeedItem item )
        {
            _unitOfWork.Feed.Update(currentApplicationUserId, feed, item);
            return feed;
        }
        #endregion

        #region Remove

        public void RemoveFeedCommunity( Feed feed, Community community )
        {
            var feedCommunity = _unitOfWork.FeedCommunity.GetFirstOrDefault(x => x.FeedId == feed.Id && x.CommunityId == community.Id);
            RemoveFeedCommunity(feedCommunity);
        }

        public async Task RemoveFeedCommunityAsync( Feed feed, Community community )
        {
            var feedCommunity = await _unitOfWork.FeedCommunity.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.CommunityId == community.Id);
            RemoveFeedCommunity(feedCommunity);
        }

        public void RemoveFeedCommunity(FeedCommunity feedCommunity)
        {
            _unitOfWork.FeedCommunity.Remove(feedCommunity);
        }

        public void RemoveFeedCommunityStateCountryRelationships(Feed feed, Community community)
        {
            var communityFromDb = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == community.Id, includeProperties: "CommunityState,CommunityState.State,CommunityState.State.Country");
            if ( communityFromDb != null )
            {
                RemoveFeedCommunity(feed, communityFromDb);
                if ( communityFromDb.CommunityState != null && communityFromDb.CommunityState.State != null )
                {
                    RemoveFeedState(feed, communityFromDb.CommunityState.State);

                    if ( communityFromDb.CommunityState.State.Country != null )
                    {
                        RemoveFeedCountry(feed, communityFromDb.CommunityState.State.Country);
                    }
                }
            }
        }

        public async Task RemoveFeedCommunityStateCountryRelationshipsAsync( Feed feed, Community community )
        {
            var communityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == community.Id, includeProperties: "CommunityState,CommunityState.State,CommunityState.State.Country");
            if ( communityFromDb != null )
            {
                await RemoveFeedCommunityAsync(feed, communityFromDb);
                if ( communityFromDb.CommunityState != null && communityFromDb.CommunityState.State != null )
                {
                    await RemoveFeedStateAsync(feed, communityFromDb.CommunityState.State);

                    if ( communityFromDb.CommunityState.State.Country != null )
                    {
                        await RemoveFeedCountryAsync(feed, communityFromDb.CommunityState.State.Country);
                    }
                }
            }
        }

        public void RemoveFeedCookbook( Feed feed, Cookbook cookbook )
        {
            var feedCookbook = _unitOfWork.FeedCookbook.GetFirstOrDefault(x => x.FeedId == feed.Id && x.CookbookId == cookbook.Id);
            RemoveFeedCookbook(feedCookbook);
        }
        public async Task RemoveFeedCookbookAsync( Feed feed, Cookbook cookbook )
        {
            var feedCookbook = await _unitOfWork.FeedCookbook.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.CookbookId == cookbook.Id);
            RemoveFeedCookbook(feedCookbook);
        }

        public void RemoveFeedCookbook(FeedCookbook feedCookbook )
        {
            _unitOfWork.FeedCookbook.Remove(feedCookbook);
        }

        public void RemoveFeedState(Feed feed, State state )
        {
            var feedState = _unitOfWork.FeedState.GetFirstOrDefault(x => x.FeedId == feed.Id && x.StateId == state.Id);
            RemoveFeedState(feedState);
        }

        public async Task RemoveFeedStateAsync( Feed feed, State state )
        {
            var feedState = await _unitOfWork.FeedState.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.StateId == state.Id);
            RemoveFeedState(feedState);
        }

        public void RemoveFeedState( FeedState feedState )
        {
            _unitOfWork.FeedState.Remove(feedState);
        }
        public void RemoveFeedCountry(Feed feed, Country country )
        {
            var feedCountry = _unitOfWork.FeedCountry.GetFirstOrDefault(x => x.FeedId == feed.Id && x.CountryId == country.Id);
            RemoveFeedCountry(feedCountry);
        }

        public async Task RemoveFeedCountryAsync( Feed feed, Country country )
        {
            var feedCountry = await _unitOfWork.FeedCountry.GetFirstOrDefaultAsync(x => x.FeedId == feed.Id && x.CountryId == country.Id);
            RemoveFeedCountry(feedCountry);
        }
        public void RemoveFeedCountry(FeedCountry feedCountry )
        {
            _unitOfWork.FeedCountry.Remove(feedCountry);
        }
        #endregion
    }
}
