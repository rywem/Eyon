using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Enums;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Security
{
    public class FeedSecurity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FeedOrchestrator _feedOrchestrator;
        public FeedSecurity( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this._feedOrchestrator = new FeedOrchestrator(this._unitOfWork);
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
