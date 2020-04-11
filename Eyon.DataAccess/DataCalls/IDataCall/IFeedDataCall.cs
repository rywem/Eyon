using Eyon.Models;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.DataCalls.IDataCall
{
    public interface IFeedDataCall
    {
        Task<Feed> AddFeedWithRelationship( string currentApplicationUserId, IFeedItem feedItem, bool saveOnRelationshipInsert = true );
        FeedCommunity AddFeedCommunity( Feed feed, Community community );

        FeedState AddFeedState( Feed feed, State state );
        Feed UpdateFeed( string currentApplicationUserId, Feed feed, IFeedItem item );
        FeedCountry AddFeedCountry( Feed feed, Country country );
        FeedCookbook AddFeedCookbook( Feed feed, Cookbook cookbook );
        FeedCategory AddFeedCategory( Feed feed, Category category );
        FeedTopic AddFeedTopic( Feed feed, Topic topic );
        FeedRecipe AddFeedRecipe( Feed feed, Recipe recipe );
        void RemoveFeedCategory( Feed feed, Category category );

        void RemoveFeedCommunityStateCountryRelationships( Feed feed, Community community );
        void RemoveFeedCommunity( FeedCommunity feedCommunity );
        Task RemoveFeedCommunityAsync( Feed feed, Community community );
        void RemoveFeedCommunity( Feed feed, Community community );

        void RemoveFeedCategory( FeedCategory feedCategory );
        void RemoveFeedTopic( FeedTopic feedTopic );
        void RemoveFeedTopic( Feed feed, Topic topic );
        Task RemoveFeedTopicAsync( Feed feed, Topic topic );
        void RemoveFeedRecipe( FeedRecipe feedRecipe );
        void RemoveFeedRecipe( Feed feed, Recipe recipe );
        Task RemoveFeedRecipeAsync( Feed feed, Recipe recipe );
    }
}
