using Eyon.DataAccess.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Enums;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Eyon.DataAccess.Orchestrators.IOrchestrator;
using Eyon.DataAccess.Security.ISecurity;

namespace Eyon.DataAccess.Security
{
    public class FeedSecurity : IFeedSecurity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedOrchestrator _feedOrchestrator;
        public FeedSecurity( IUnitOfWork unitOfWork, IFeedOrchestrator feedOrchestrator )
        {
            this._unitOfWork = unitOfWork;
            this._feedOrchestrator = feedOrchestrator;
        }

        public async Task<FeedViewModel> GetFeedAsync(string currentApplicationUserId = null, FeedSortBy sortBy = FeedSortBy.New, int skip = 0, int take = 0)
        {
            if ( currentApplicationUserId == null )
            {
                return await this._feedOrchestrator.GetPublicFeedViewModel(sortBy, skip, take );
            }
            else
                throw new NotImplementedException();
        }

        public async Task AddAsync( string currentApplicationUserId, FeedItemViewModel feedItemViewModel, bool useTransaction = true )
        {
            if ( string.IsNullOrEmpty(currentApplicationUserId) )
                throw new SafeException(ErrorType.AnErrorOccurred, new Exception("Current Application User ID is empty FeedSecurity.AddAsync()"));

            if ( useTransaction )
                await _feedOrchestrator.AddTransactionAsync(currentApplicationUserId, feedItemViewModel);
            else
                await _feedOrchestrator.AddAsync(currentApplicationUserId, feedItemViewModel);
        }

        public async Task DeleteAsync(string currentApplicationUserId, long feedId, bool useTransaction = true)
        {
            var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == feedId, includeProperties: "ApplicationUserOwner,FeedCommunity,FeedState,FeedOrganization,FeedCategory,FeedCountry,FeedCookbook,FeedRecipe,FeedProfile,FeedTopic");

            if ( feedFromDb == null )
                throw new SafeException("An error occurred.", new Exception(string.Format("Owned feed item not found. Feed ID {0},  Current application user ID {1}", feedId, currentApplicationUserId)));

            if (useTransaction == true )
                await this._feedOrchestrator.DeleteTransactionAsync(feedFromDb);
            else
                await this._feedOrchestrator.DeleteAsync(feedFromDb);
        }
    }
}
