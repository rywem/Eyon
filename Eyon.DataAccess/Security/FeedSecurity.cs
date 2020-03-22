using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
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
