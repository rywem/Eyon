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
                includeProperties: "CommunityRecipe,CommunityRecipe.Community,Instructions,RecipeIngredient,RecipeIngredient.Ingredient,CookbookRecipes,CookbookRecipes.Cookbook"); //RecipeCategories,RecipesCategories.Category,

            if ( recipeViewModel.Recipe != null )
            {
                if ( recipeViewModel.Recipe.CommunityRecipe != null )
                    recipeViewModel.Community = recipeViewModel.Recipe.CommunityRecipe.Community;

                if ( recipeViewModel.Recipe.Instructions != null && recipeViewModel.Recipe.Instructions.Count > 0 )
                    recipeViewModel.Instructions = recipeViewModel.Recipe.Instructions.ToList();

                if ( recipeViewModel.Recipe.RecipeIngredient != null && recipeViewModel.Recipe.RecipeIngredient.Count > 0 )
                    recipeViewModel.Ingredients = recipeViewModel.Recipe.RecipeIngredient.Select(x => x.Ingredient).ToList();

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
            List<Task> tasks = new List<Task>();
            if ( await _unitOfWork.Recipe.IsOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe.Id) == false )
            {
                throw new WebUserSafeException("Access denied.");
            }
            var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "Instructions");            
            if ( recipeFromDb == null )
            {
                return;
            }


            await _unitOfWork.Recipe.UpdateIfOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe);
            //tasks.Add(_unitOfWork.Recipe.UpdateIfOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe));
            List<Instruction> instructionsToRemove = new List<Instruction>();

            if ( recipeViewModel.Instructions.Count < recipeFromDb.Instructions.Count )
            {
                var dbInstructionsList = recipeFromDb.Instructions.ToList();
                for ( int i = recipeViewModel.Instructions.Count; i < recipeFromDb.Instructions.Count; i++ )
                {
                    var itemToRemove = dbInstructionsList[i];
                    _unitOfWork.Instruction.Remove(itemToRemove.Id);
                    //tasks.Add(_unitOfWork.SaveAsync());
                }
            }
            foreach ( var item in recipeViewModel.Instructions )
            {
                var instructionFromDb = recipeFromDb.Instructions.FirstOrDefault(x => x.StepNumber == item.StepNumber);

                if(instructionFromDb == null ) // add
                {
                    _unitOfWork.Instruction.Add(item);                    
                }
                else
                {
                    if ( !instructionFromDb.Text.Equals(item.Text) )
                    {
                        instructionFromDb.Text = item.Text;
                        tasks.Add(_unitOfWork.Instruction.UpdateAsync(instructionFromDb));
                    }
                }
            }
            //tasks.Add(_unitOfWork.SaveAsync());
            await _unitOfWork.SaveAsync();

            // Update Instructions, Add Instructions/Remove Instructions

            // Update Ingredients, Add Ingredients/Remove Ingredients

            // Update Community

            // Update cookbooks, add cookbooks/remove cookbooks

            // Update RecipeSiteImages,  add/remove

            // Update Categories: Add Relations, remove relations       
            //Task.WaitAll(tasks.ToArray());
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
