using Eyon.DataAccess.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Eyon.Models.ViewModels;

namespace Eyon.DataAccess.Security
{
    public class CookbookSecurity
    {
        private readonly IUnitOfWork _unitOfWork;
        private CookbookOrchestrator _cookbookOrchestrator;

        public CookbookSecurity( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this._cookbookOrchestrator = new CookbookOrchestrator(unitOfWork);
        }

        public async Task<bool> DeleteAsync( string currentApplicationUserId, long id )
        {
            var objFromDb = await _unitOfWork.Cookbook.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == id
                , includeProperties: "CommunityCookbook,OrganizationCookbook,CookbookRecipe,CookbookCategory,ApplicationUserOwner,FeedCookbook,FeedCookbook.Feed");

            if ( objFromDb == null )
                return false;

            await _cookbookOrchestrator.DeleteTransactionAsync(currentApplicationUserId, objFromDb);
            return true;

        }

        public async Task AddAsync(string currentApplicationUserId, CookbookViewModel cookbookViewModel)
        {
            await _cookbookOrchestrator.AddTransactionAsync(currentApplicationUserId, cookbookViewModel);
        }

        public async Task UpdateAsync( string currentApplicationUserId, CookbookViewModel cookbookViewModel )
        {
            bool isOwner = await _unitOfWork.Cookbook.IsOwnerAsync(currentApplicationUserId, cookbookViewModel.Cookbook.Id);

            if ( isOwner )
                _cookbookOrchestrator.UpdateCookbookTransaction(currentApplicationUserId, cookbookViewModel);
        }
    }
}
