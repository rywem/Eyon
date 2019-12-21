using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Eyon.DataAccess.Data.Orchestrators
{
    public class RecipeOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecipeOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }

        public RecipeViewModel GetRecipeViewModel(long id)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel.Recipe = _unitOfWork.Recipe.GetFirstOrDefault(x => x.Id == id,
                includeProperties: "CommunityRecipe,CommunityRecipe.Community,Instructions,RecipeIngredient,RecipeIngredient.Ingredient,RecipeCategories,RecipesCategories.Category,CookbookRecipes,CookbookRecipes.Cookbook");

            if ( recipeViewModel.Recipe != null )
            {
                if ( recipeViewModel.Recipe.CommunityRecipe != null )
                    recipeViewModel.Community = recipeViewModel.Recipe.CommunityRecipe.Community;

                if ( recipeViewModel.Recipe.Instructions != null && recipeViewModel.Recipe.Instructions.Count > 0)
                    recipeViewModel.Instructions = recipeViewModel.Recipe.Instructions.ToList();
                
                if ( recipeViewModel.Recipe.RecipeIngredient != null && recipeViewModel.Recipe.RecipeIngredient.Count > 0)
                    recipeViewModel.Ingredients = recipeViewModel.Recipe.RecipeIngredient.Select(x => x.Ingredient).ToList();
                
                if ( recipeViewModel.Recipe.RecipeCategories != null && recipeViewModel.Recipe.RecipeCategories.Count > 0)
                    recipeViewModel.Categories = recipeViewModel.Recipe.RecipeCategories.Select(x => x.Category).ToList();
                
                if ( recipeViewModel.Recipe.RecipeSiteImages != null && recipeViewModel.Recipe.RecipeSiteImages.Count > 0 )
                    recipeViewModel.RecipeSiteImages = recipeViewModel.Recipe.RecipeSiteImages.Select(x => x.SiteImage).ToList();
                
                if ( recipeViewModel.Recipe.CookbookRecipes != null && recipeViewModel.Recipe.CookbookRecipes.Count > 0 )
                    recipeViewModel.Cookbooks = recipeViewModel.Recipe.CookbookRecipes.Select(x => x.Cookbook).ToList();
            }
            return recipeViewModel;
        }
    }
}
