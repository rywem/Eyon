using Eyon.Core.Orchestrators;
using Eyon.Core.Data.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Eyon.Models.ViewModels;
using Eyon.Core.Orchestrators.IOrchestrator;
using Eyon.Core.Security.ISecurity;

namespace Eyon.Core.Security
{
    public class CookbookSecurity : ICookbookSecurity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICookbookOrchestrator _cookbookOrchestrator;

        public CookbookSecurity( IUnitOfWork unitOfWork, ICookbookOrchestrator cookbookOrchestator  )
        {
            this._unitOfWork = unitOfWork;
            this._cookbookOrchestrator = cookbookOrchestator;
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
                await _cookbookOrchestrator.UpdateTransactionAsync(currentApplicationUserId, cookbookViewModel);
        }
    }
}
