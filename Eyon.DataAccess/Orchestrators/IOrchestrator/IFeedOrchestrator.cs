using Eyon.Models;
using Eyon.Models.Enums;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Orchestrators.IOrchestrator
{
    public interface IFeedOrchestrator
    {
        Task<Models.ViewModels.FeedViewModel> GetPublicFeedViewModel( FeedSortBy sortBy = FeedSortBy.New, int skip = 0, int take = 0 );

        Task AddTransactionAsync( string currentApplicationUserId, FeedItemViewModel feedViewModel );
        Task AddAsync( string currentApplicationUserId, FeedItemViewModel feedViewModel);
        Task DeleteTransactionAsync( Feed feed );
        Task DeleteAsync( Feed feed );
        Task UpdateAsync( string currentApplicationUserId, FeedItemViewModel feedItemViewModel );
        Task UpdateTransactionAsync( string currentApplicationUserId, FeedItemViewModel feedItemViewModel );
    }
}
