using Eyon.Models.Enums;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Security.ISecurity
{
    public interface IFeedSecurity
    {
        Task<FeedViewModel> GetFeedAsync( string currentApplicationUserId = null, FeedSortBy sortBy = FeedSortBy.New, int skip = 0, int take = 0 );
        Task AddAsync( string currentApplicationUserId, FeedItemViewModel feedViewModel, bool useTransaction = true );
        Task DeleteAsync( string currentApplicationUserId, long feedId, bool useTransaction = true );
    }
}
