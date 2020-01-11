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
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Orchestrators
{
    public class RecipeOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecipeOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }

        public RecipeViewModel GetRecipeViewModel(long id, string currentApplicationUserId, string includeProperties = null)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();            
            recipeViewModel.Recipe = _unitOfWork.Recipe.GetFirstOrDefault( x => x.Id == id,
                includeProperties: "CommunityRecipe,CommunityRecipe,Instruction,Ingredient,CookbookRecipe,CookbookRecipe.Cookbook,RecipeUserImage,RecipeUserImage.UserImage", false); 

            if ( recipeViewModel.Recipe != null )
            {
                // check if it is private, if so, only allow owners to view.
                if ( recipeViewModel.Recipe.Privacy == Models.Enums.Privacy.Private
                    && _unitOfWork.Recipe.IsOwner(currentApplicationUserId, recipeViewModel.Recipe.Id) == false )
                {
                    throw new WebUserSafeException("An error occurred.");
                }

                if ( recipeViewModel.Recipe.CommunityRecipe != null )
                {
                    recipeViewModel.Community = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == recipeViewModel.Recipe.CommunityRecipe.CommunityId, includeProperties: "CommunityState,CommunityState.State,Country");
                    recipeViewModel.CommunityName = Eyon.Models.Helpers.CommunityHelper.FullNameFormatter(recipeViewModel.Community);
                    recipeViewModel.CommunityId = recipeViewModel.Community.Id;
                }
                if ( recipeViewModel.Recipe.Instruction != null && recipeViewModel.Recipe.Instruction.Count > 0 )
                { 
                    recipeViewModel.Instruction = recipeViewModel.Recipe.Instruction.ToList();                    
                    recipeViewModel.InstructionsText = string.Join(Environment.NewLine, recipeViewModel.Instruction.OrderBy(x => x.StepNumber).Select(x => x.Text));                    
                }

                if ( recipeViewModel.Recipe.Ingredient != null && recipeViewModel.Recipe.Ingredient.Count > 0 )
                {
                    recipeViewModel.Ingredient = recipeViewModel.Recipe.Ingredient.ToList();
                    recipeViewModel.IngredientsText = string.Join(Environment.NewLine, recipeViewModel.Recipe.Ingredient.ToList().Select(x => x.Text));
                }

                if ( recipeViewModel.Recipe.RecipeCategory != null && recipeViewModel.Recipe.RecipeCategory.Count > 0 )
                    recipeViewModel.Category = recipeViewModel.Recipe.RecipeCategory.Select(x => x.Category).ToList();

                if ( recipeViewModel.Recipe.RecipeUserImage != null && recipeViewModel.Recipe.RecipeUserImage.Count > 0 )
                    recipeViewModel.UserImage = recipeViewModel.Recipe.RecipeUserImage.Select(x => x.UserImage).ToList();

                if ( recipeViewModel.Recipe.CookbookRecipe != null && recipeViewModel.Recipe.CookbookRecipe.Count > 0 )
                    recipeViewModel.Cookbooks = recipeViewModel.Recipe.CookbookRecipe.Select(x => x.Cookbook).ToList();
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
            _unitOfWork.Recipe.AddOwnerRelationship(currentApplicationUserId, recipeViewModel.Recipe, new ApplicationUserRecipe());
            await _unitOfWork.SaveAsync();

            var communityRecipe = new CommunityRecipe()
            {
                CommunityId = communityFromDb.Id,
                RecipeId = recipeViewModel.Recipe.Id
            };
            _unitOfWork.CommunityRecipe.Add(communityRecipe);
            await _unitOfWork.SaveAsync();
            // Add instructions
            foreach ( var item in recipeViewModel.Instruction )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Instruction.Add(item);
            }            

            foreach ( var item in recipeViewModel.Ingredient )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Ingredient.Add(item);
            }            
            await _unitOfWork.SaveAsync();

            await AddRecipeUserImageAsync(currentApplicationUserId, recipeViewModel);
        }


        /// <summary>
        /// Users do not need to own the recipe to add the recipe image
        /// </summary>
        /// <param name="recipeViewModel"></param>
        /// <returns></returns>
        private async Task AddRecipeUserImageAsync(string currentApplicationUserId, RecipeViewModel recipeViewModel)
        {

            if ( recipeViewModel.UserImage != null && recipeViewModel.UserImage.Count > 0 )
            {
                var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id);
                var applicationUserFromDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(x => x.Id == currentApplicationUserId);
                if ( recipeFromDb != null && applicationUserFromDb != null )
                {
                    foreach ( var item in recipeViewModel.UserImage )
                    {
                        if ( item.Id == 0 )
                            _unitOfWork.UserImage.Add(item);
                    }
                    await _unitOfWork.SaveAsync();

                    foreach ( var item in recipeViewModel.UserImage )
                    {
                        _unitOfWork.RecipeUserImage.Add(new RecipeUserImage()
                        {
                            RecipeId = recipeFromDb.Id,
                            UserImageId = item.Id
                        });
                        _unitOfWork.ApplicationUserUserImage.Add(new ApplicationUserUserImage()
                        {
                            ApplicationUserId = currentApplicationUserId,
                            ObjectId = item.Id
                        });
                    }
                    await _unitOfWork.SaveAsync();
                }
            }
        }

        public void AddRecipe( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            _unitOfWork.Recipe.Add(recipeViewModel.Recipe);
            _unitOfWork.Save();
            _unitOfWork.Recipe.AddOwnerRelationship(currentApplicationUserId, recipeViewModel.Recipe, new Models.Relationship.ApplicationUserRecipe());
            _unitOfWork.Save();
            // Add instructions

            foreach ( var item in recipeViewModel.Instruction )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Instruction.Add(item);
            }
            // Add ingregients
            foreach ( var item in recipeViewModel.Ingredient )
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
            var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CommunityRecipe,Instructions,Ingredients");            
            if ( recipeFromDb == null )
            {
                return;
            }
            //await _unitOfWork.Recipe.UpdateIfOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe);
            _unitOfWork.Recipe.UpdateIfOwner(currentApplicationUserId, recipeViewModel.Recipe);
            await _unitOfWork.SaveAsync();
            // instructions

            // remove instructions
            if ( recipeViewModel.Instruction.Count < recipeFromDb.Instruction.Count )
            {
                var dbInstructionsList = recipeFromDb.Instruction.ToList();
                for ( int i = recipeViewModel.Instruction.Count; i < recipeFromDb.Instruction.Count; i++ )
                {
                    var itemToRemove = dbInstructionsList[i];
                    _unitOfWork.Instruction.Remove(itemToRemove.Id);
                }
            }
            // add or update new instructions
            foreach ( var item in recipeViewModel.Instruction )
            {
                var instructionFromDb = recipeFromDb.Instruction.FirstOrDefault(x => x.StepNumber == item.StepNumber);

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
            if ( recipeViewModel.Ingredient.Count < recipeFromDb.Ingredient.Count )
            {
                var dbIngredientsList = recipeFromDb.Ingredient.ToList();
                
                for ( int i = recipeViewModel.Ingredient.Count; i < recipeFromDb.Ingredient.Count; i++ )
                {
                    var itemToRemove = dbIngredientsList[i];
                    _unitOfWork.Ingredient.Remove(itemToRemove.Id);                    
                }
            }
            int ingredientCounter = 0;

            // add or update ingredients            
            foreach ( var item in recipeViewModel.Ingredient )
            {
                var ingredientFromDb = recipeFromDb.Ingredient.FirstOrDefault(x => x.Number == item.Number );
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
            if ( recipeFromDb.CommunityRecipe == null && recipeViewModel.CommunityId > 0 )
            {
                var newCommunityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.CommunityId);
                var communityRecipe = new CommunityRecipe()
                {
                    CommunityId = newCommunityFromDb.Id,
                    RecipeId = recipeFromDb.Id
                };
                _unitOfWork.CommunityRecipe.Add(communityRecipe);
                await _unitOfWork.SaveAsync();
            }
            else if ( recipeFromDb.CommunityRecipe != null && recipeFromDb.CommunityRecipe != null & recipeViewModel.CommunityId != recipeFromDb.CommunityRecipe.CommunityId )
            {
                var newCommunityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.CommunityId);
                if ( newCommunityFromDb == null )
                    throw new WebUserSafeException("An error occurred");

                _unitOfWork.CommunityRecipe.Remove(recipeFromDb.CommunityRecipe);
                _unitOfWork.CommunityRecipe.Add(new CommunityRecipe() { RecipeId = recipeFromDb.Id, CommunityId = newCommunityFromDb.Id });
                await _unitOfWork.SaveAsync();
            }
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
