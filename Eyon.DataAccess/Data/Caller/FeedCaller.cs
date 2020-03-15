using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

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

        public FeedCommunity AddFeedCommunity(Feed feed, Community community)
        {
            var feedCommunity = new FeedCommunity() { CommunityId = community.Id, FeedId = feed.Id };
            _unitOfWork.FeedCommunity.Add(feedCommunity);
            return feedCommunity;
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

        public void RemoveFeedCommunity(FeedCommunity feedCommunity)
        {
            _unitOfWork.FeedCommunity.Remove(feedCommunity);
        }
        #endregion
    }
}
