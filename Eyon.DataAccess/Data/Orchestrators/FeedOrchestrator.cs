using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Orchestrators
{
    public class FeedOrchestrator
    {

        private readonly IUnitOfWork _unitOfWork;
        public FeedOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }
        public FeedViewModel GetPublicFeedViewModel()
        {
            throw new NotImplementedException();
            FeedViewModel feedViewModel = new FeedViewModel();
            //feedViewModel.Feed = _unitOfWork.Feed.Get
            return feedViewModel;
        }

        public async Task DeleteAsync( Feed feed )
        {
            //var feed = await _unitOfWork.Feed.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == feed.Id, includeProperties: "ApplicationUserOwner,FeedCommunity,FeedState,FeedOrganization,FeedCategory,FeedCountry,FeedCookbook,FeedRecipe,FeedProfile,FeedTopic");

            //if ( feedFromDb == null )
            //  throw new SafeException("An error occurred.", new Exception(string.Format("Owned feed item not found. Feed ID {0},  Current application user ID {1}", feed.Id, currentApplicationUserId)));

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
