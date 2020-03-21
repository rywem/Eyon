using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Security
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

        public async Task DeleteAsync(string currentApplicationUserId, long id )
        {
            var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == id, includeProperties: "ApplicationUserOwner,CommunityRecipe,CommunityRecipe,Instruction,Ingredient,CookbookRecipe,RecipeUserImage,RecipeUserImage.UserImage,FeedRecipe,RecipeCategory", false);

            if ( recipeFromDb == null )
                throw new SafeException("An error occurred.", new Exception(string.Format("Owned item not found. Recipe ID {0},  Current application user ID {1}", id, currentApplicationUserId)));

            await this._recipeOrchestrator.DeleteRecipeTransactionAsync(currentApplicationUserId, recipeFromDb);            
        }
    }
}
