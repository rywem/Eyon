using Eyon.DataAccess.Data.Repository.IRepository;
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
using Eyon.DataAccess.Orchestrators.IOrchestrator;
using Eyon.DataAccess.DataCalls.IDataCall;

namespace Eyon.DataAccess.Orchestrators
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

    }
}
