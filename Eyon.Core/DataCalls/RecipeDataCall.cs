using Eyon.Core.Data.Repository.IRepository;
using Eyon.Core.DataCalls.IDataCall;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.Core.DataCalls
{
    public class RecipeDataCall : IRecipeDataCall
    {
        private IUnitOfWork _unitOfWork;

        public RecipeDataCall(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Recipe> AddRecipeWithRelationship( string currentApplicationUserId, Recipe recipe, bool saveOnRelationshipInsert = true )
        {
            _unitOfWork.Recipe.Add(recipe);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Recipe.AddOwnerRelationship(currentApplicationUserId, recipe, new ApplicationUserRecipe());

            if ( saveOnRelationshipInsert == true )
                await _unitOfWork.SaveAsync();

            return recipe;
        }
        public CommunityRecipe AddCommunityRecipe( Community community, Recipe recipe )
        {
            return _unitOfWork.CommunityRecipe.AddFromEntities(community, recipe);
        }

        public CookbookRecipe AddCookbookRecipe( Cookbook cookbook, Recipe recipe )
        {
            return _unitOfWork.CookbookRecipe.AddFromEntities(cookbook, recipe);
        }


        public RecipeCategory AddRecipeCategory( Recipe recipe, Category category )
        {
            return _unitOfWork.RecipeCategory.AddFromEntities(recipe, category);
        }

        public Ingredient AddIngredient( Ingredient ingredient )
        {
            _unitOfWork.Ingredient.Add(ingredient);
            return ingredient;
        }

        public Ingredient AddIngredient( Ingredient ingredient, Recipe recipe )
        {
            ingredient.RecipeId = recipe.Id;
            return AddIngredient(ingredient);
        }

        public Instruction AddInstruction( Instruction instruction )
        {
            _unitOfWork.Instruction.Add(instruction);
            return instruction;
        }

        public Instruction AddInstruction( Instruction instruction, Recipe recipe )
        {
            instruction.RecipeId = recipe.Id;
            return AddInstruction(instruction);
        }

        public Ingredient UpdateIngredient( Ingredient ingredient, string newText )
        {
            ingredient.Text = newText;
            _unitOfWork.Ingredient.Update(ingredient);
            return ingredient;
        }

        public Instruction UpdateInstruction( Instruction instruction, string newText )
        {
            instruction.Text = newText;
            _unitOfWork.Instruction.Update(instruction);
            return instruction;
        }

        public void RemoveCommunityRecipe( CommunityRecipe communityRecipe )
        {
            _unitOfWork.CommunityRecipe.Remove(communityRecipe);
        }

        public void RemoveCommunityRecipe( Community community, Recipe recipe )
        {
            var objFromDb = _unitOfWork.CommunityRecipe.GetFirstOrDefault(x => x.CommunityId == community.Id && x.RecipeId == recipe.Id);

            if ( objFromDb != null )
                RemoveCommunityRecipe(objFromDb);
        }

        public async Task RemoveCommunityRecipeAsync( Community community, Recipe recipe )
        {
            var objFromDb = await _unitOfWork.CommunityRecipe.GetFirstOrDefaultAsync(x => x.CommunityId == community.Id && x.RecipeId == recipe.Id);

            if ( objFromDb != null )
                RemoveCommunityRecipe(objFromDb);
        }

        public void RemoveCookbookRecipe( CookbookRecipe cookbookRecipe )
        {
            _unitOfWork.CookbookRecipe.Remove(cookbookRecipe);
        }

        public void RemoveCookbookRecipe( Cookbook cookbook, Recipe recipe )
        {
            var cookbookRecipe = _unitOfWork.CookbookRecipe.GetFirstOrDefault(x => x.CookbookId == cookbook.Id && x.RecipeId == recipe.Id);

            if ( cookbookRecipe != null )
                RemoveCookbookRecipe(cookbookRecipe);
        }

        public async Task RemoveCookbookRecipeAsync( Cookbook cookbook, Recipe recipe )
        {
            var cookbookRecipe = await _unitOfWork.CookbookRecipe.GetFirstOrDefaultAsync(x => x.CookbookId == cookbook.Id && x.RecipeId == recipe.Id);

            if ( cookbookRecipe != null )
                RemoveCookbookRecipe(cookbookRecipe);
        }

        public void RemoveInstruction(Instruction instruction)
        {
            RemoveInstruction(instruction.Id);
        }
        public void RemoveInstruction( long idToRemove )
        {
            _unitOfWork.Instruction.Remove(idToRemove);
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            RemoveIngredient(ingredient.Id);
        }
        public void RemoveIngredient( long idToRemove )
        {
            _unitOfWork.Ingredient.Remove(idToRemove);
        }


        public void RemoveRecipeCategory( RecipeCategory recipeCategory )
        {
            _unitOfWork.RecipeCategory.Remove(recipeCategory);
        }

        public void RemoveRecipeCategory( Recipe recipe, Category category )
        {
            var objFromDb = _unitOfWork.RecipeCategory.GetFirstOrDefault(x => x.CategoryId == category.Id && x.RecipeId == recipe.Id);

            if ( objFromDb != null )
                RemoveRecipeCategory(objFromDb);
        }

        public async Task RemoveRecipeCategoryAsync( Recipe recipe, Category category )
        {
            var objFromDb = await _unitOfWork.RecipeCategory.GetFirstOrDefaultAsync(x => x.CategoryId == category.Id && x.RecipeId == recipe.Id);

            if ( objFromDb != null )
                RemoveRecipeCategory(objFromDb);
        }
    }
}
