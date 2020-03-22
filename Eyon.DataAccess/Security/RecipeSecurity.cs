using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Security
{
    public class RecipeSecurity
    {
        private readonly IUnitOfWork _unitOfWork;
        private RecipeOrchestrator _recipeOrchestrator;
        private FeedSecurity _feedSecurity; 
        public RecipeSecurity( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this._recipeOrchestrator = new RecipeOrchestrator(this._unitOfWork);
            this._feedSecurity = new FeedSecurity(this._unitOfWork);
        }

        public async Task<RecipeViewModel> GetAsync(string currentApplicationUserId, long id )
        {
            // check that the Recipe exists.
            if ( await _unitOfWork.Recipe.AnyAsync(x => x.Id == id) )
            {
                // check privacy, if private, only an owner can access.
                if ( await _unitOfWork.Recipe.UserCanViewAsync(currentApplicationUserId, id) )
                {
                    // determine if this is the current owner to render the correct UI components
                    return await this._recipeOrchestrator.GetAsync(currentApplicationUserId, id);
                }
                else
                    throw new SafeException(Models.Enums.ErrorType.Denied);
            }
            else
            {
                throw new SafeException(Models.Enums.ErrorType.NotFound);
            }
        }

        public async Task UpdateAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            // Ensure ownership of recipe record
            if ( await _unitOfWork.Recipe.IsOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe.Id) )
            {
                await _recipeOrchestrator.UpdateTransactionAsync(currentApplicationUserId, recipeViewModel);
            }
            else
            {
                throw new SafeException(Models.Enums.ErrorType.Denied, new Exception(string.Format("Attempted to update recipe, but was not the owner. Violating userId {0}, Attempted to update Recipe ID {1}", currentApplicationUserId, recipeViewModel.Recipe.Id)));                
            }
        }

        public async Task AddAsync(string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            await this._recipeOrchestrator.AddTransactionAsync(currentApplicationUserId, recipeViewModel);
        }

        public async Task DeleteAsync(string currentApplicationUserId, long id )
        {
            var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == id, includeProperties: "ApplicationUserOwner,CommunityRecipe,CommunityRecipe,Instruction,Ingredient,CookbookRecipe,RecipeUserImage,RecipeUserImage.UserImage,FeedRecipe,RecipeCategory", false);

            if ( recipeFromDb == null )
                throw new SafeException(Models.Enums.ErrorType.Denied, new Exception(string.Format("Owned item not found. Recipe ID {0},  Current application user ID {1}", id, currentApplicationUserId)));

            await this._recipeOrchestrator.DeleteTransactionAsync(currentApplicationUserId, recipeFromDb);            
        }
    }
}
