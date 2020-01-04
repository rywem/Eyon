using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Eyon.Models;
using Eyon.DataAccess.Data.Repository;
using System.Threading.Tasks;
using Eyon.Models.Errors;

namespace Eyon.DataAccess.Data.Orchestrators
{
    public class RecipeOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecipeOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }

        public RecipeViewModel GetRecipeViewModel(long id, string currentApplicationUserId)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel.Recipe = _unitOfWork.Recipe.GetFirstOrDefaultOwned(currentApplicationUserId, x => x.Id == id,
                includeProperties: "CommunityRecipe,CommunityRecipe,Instructions,Ingredients,CookbookRecipes,CookbookRecipes.Cookbook"); //RecipeCategories,RecipesCategories.Category,

            if ( recipeViewModel.Recipe != null )
            {
                if ( recipeViewModel.Recipe.CommunityRecipe != null )
                {
                    recipeViewModel.Community = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == recipeViewModel.Recipe.CommunityRecipe.CommunityId, includeProperties: "CommunityState,CommunityState.State,Country");
                    recipeViewModel.CommunityName = Eyon.Models.Helpers.CommunityHelper.FullNameFormatter(recipeViewModel.Community);
                    recipeViewModel.CommunityId = recipeViewModel.Community.Id;
                }
                if ( recipeViewModel.Recipe.Instructions != null && recipeViewModel.Recipe.Instructions.Count > 0 )
                { 
                    recipeViewModel.Instructions = recipeViewModel.Recipe.Instructions.ToList();                    
                    recipeViewModel.InstructionsText = string.Join(Environment.NewLine, recipeViewModel.Instructions.OrderBy(x => x.StepNumber).Select(x => x.Text));
                }

                if ( recipeViewModel.Recipe.Ingredients != null && recipeViewModel.Recipe.Ingredients.Count > 0 )
                {
                    recipeViewModel.Ingredients = recipeViewModel.Recipe.Ingredients.ToList();
                    recipeViewModel.IngredientsText = string.Join(Environment.NewLine, recipeViewModel.Ingredients.Select(x => x.Text));
                }

                if ( recipeViewModel.Recipe.RecipeCategories != null && recipeViewModel.Recipe.RecipeCategories.Count > 0 )
                    recipeViewModel.Categories = recipeViewModel.Recipe.RecipeCategories.Select(x => x.Category).ToList();

                if ( recipeViewModel.Recipe.RecipeSiteImages != null && recipeViewModel.Recipe.RecipeSiteImages.Count > 0 )
                    recipeViewModel.RecipeSiteImages = recipeViewModel.Recipe.RecipeSiteImages.Select(x => x.SiteImage).ToList();

                if ( recipeViewModel.Recipe.CookbookRecipes != null && recipeViewModel.Recipe.CookbookRecipes.Count > 0 )
                    recipeViewModel.Cookbooks = recipeViewModel.Recipe.CookbookRecipes.Select(x => x.Cookbook).ToList();
            }
            return recipeViewModel;
        }



        public async Task AddRecipeTransactionAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await AddRecipeAsync(currentApplicationUserId, recipeViewModel);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public void AddRecipeTransaction( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    AddRecipe( currentApplicationUserId, recipeViewModel);
                    transaction.Commit();
                }
                catch ( Exception ex )
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task AddRecipeAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            var communityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.CommunityId);

            if ( communityFromDb == null )
                throw new WebUserSafeException("An error occurred");

            _unitOfWork.Recipe.Add(recipeViewModel.Recipe);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Recipe.AddOwned(currentApplicationUserId, recipeViewModel.Recipe, new Models.Relationship.ApplicationUserRecipe());
            await _unitOfWork.SaveAsync();

            // Add instructions
            foreach ( var item in recipeViewModel.Instructions )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Instruction.Add(item);
            }            

            foreach ( var item in recipeViewModel.Ingredients )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Ingredient.Add(item);
            }
            

            await _unitOfWork.SaveAsync();
        }

        public void AddRecipe( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            _unitOfWork.Recipe.Add(recipeViewModel.Recipe);
            _unitOfWork.Save();
            _unitOfWork.Recipe.AddOwned(currentApplicationUserId, recipeViewModel.Recipe, new Models.Relationship.ApplicationUserRecipe());
            _unitOfWork.Save();
            // Add instructions

            foreach ( var item in recipeViewModel.Instructions )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Instruction.Add(item);
            }
            // Add ingregients
            foreach ( var item in recipeViewModel.Ingredients )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Ingredient.Add(item);
            }
            _unitOfWork.Save();
        }

        public void UpdateRecipe(RecipeViewModel recipeViewModel)
        {
            // Update Ingredients, Add Ingredients/Remove Ingredients

            // Update Instructions, Add Instructions/Remove Instructions

            // Update Community

            // Update cookbooks, add cookbooks/remove cookbooks

            // Update RecipeSiteImages,  add/remove

            // Update Categories: Add Relations, remove relations            
        }

        public async Task UpdateRecipeAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            
            if ( await _unitOfWork.Recipe.IsOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe.Id) == false )
            {
                throw new WebUserSafeException("An error occurred.");
            }
            var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "Instructions,Ingredients");            
            if ( recipeFromDb == null )
            {
                return;
            }
            await _unitOfWork.Recipe.UpdateIfOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe);            

            // instructions

            // remove instructions
            if ( recipeViewModel.Instructions.Count < recipeFromDb.Instructions.Count )
            {
                var dbInstructionsList = recipeFromDb.Instructions.ToList();
                for ( int i = recipeViewModel.Instructions.Count; i < recipeFromDb.Instructions.Count; i++ )
                {
                    var itemToRemove = dbInstructionsList[i];
                    _unitOfWork.Instruction.Remove(itemToRemove.Id);
                }
            }
            // add or update new instructions
            foreach ( var item in recipeViewModel.Instructions )
            {
                var instructionFromDb = recipeFromDb.Instructions.FirstOrDefault(x => x.StepNumber == item.StepNumber);

                if(instructionFromDb == null ) // add
                {
                    item.RecipeId = recipeFromDb.Id;
                    _unitOfWork.Instruction.Add(item);                    
                }
                else
                {
                    if ( !instructionFromDb.Text.Equals(item.Text) )
                    {
                        instructionFromDb.Text = item.Text;
                        _unitOfWork.Instruction.Update(instructionFromDb);
                    }
                }
            }

            // ingredients

            // remove ingredients 
            if ( recipeViewModel.Ingredients.Count < recipeFromDb.Ingredients.Count )
            {
                var dbIngredientsList = recipeFromDb.Ingredients.ToList();
                
                for ( int i = recipeViewModel.Ingredients.Count; i < recipeFromDb.Ingredients.Count; i++ )
                {
                    var itemToRemove = dbIngredientsList[i];
                    _unitOfWork.Ingredient.Remove(itemToRemove.Id);                    
                }
            }
            int ingredientCounter = 0;
            // add or update ingredients
            
            foreach ( var item in recipeViewModel.Ingredients )
            {
                var ingredientFromDb = recipeFromDb.Ingredients.FirstOrDefault(x => x.Number == item.Number );
                if ( ingredientFromDb == null )
                {
                    item.RecipeId = recipeFromDb.Id;
                    _unitOfWork.Ingredient.Add(item);
                }
                else
                {
                    if ( !ingredientFromDb.Text.Equals(item.Text) )
                    {
                        ingredientFromDb.Text = item.Text;
                        _unitOfWork.Ingredient.Update(ingredientFromDb);
                    }
                }
                ingredientCounter++;
            }

            await _unitOfWork.SaveAsync();

            // Update Community

            // Update cookbooks, add cookbooks/remove cookbooks

            // Update RecipeSiteImages,  add/remove

            // Update Categories: Add Relations, remove relations       
            
        }
        public async Task UpdateRecipeTransactionAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await UpdateRecipeAsync(currentApplicationUserId, recipeViewModel);
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
