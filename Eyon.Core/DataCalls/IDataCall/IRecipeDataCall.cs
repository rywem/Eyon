using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.Core.DataCalls.IDataCall
{
    public interface IRecipeDataCall
    {
        Task<Recipe> AddRecipeWithRelationship( string currentApplicationUserId, Recipe recipe, bool saveOnRelationshipInsert = false );

        CookbookRecipe AddCookbookRecipe( Cookbook cookbook, Recipe recipe );
        CommunityRecipe AddCommunityRecipe( Community community, Recipe recipe );
        RecipeCategory AddRecipeCategory( Recipe recipe, Category category );

        Ingredient AddIngredient( Ingredient ingredient );
        Instruction AddInstruction( Instruction instruction );
        Ingredient AddIngredient( Ingredient ingredient, Recipe recipe );
        Instruction AddInstruction( Instruction instruction, Recipe recipe );
        Ingredient UpdateIngredient( Ingredient ingredient, string newText );
        Instruction UpdateInstruction( Instruction instruction, string newText );

        void RemoveInstruction( Instruction instruction );
        void RemoveInstruction( long idToRemove );
        void RemoveIngredient( Ingredient ingredient );
        void RemoveIngredient( long idToRemove );
        void RemoveCookbookRecipe( CookbookRecipe cookbookRecipe );
        void RemoveCookbookRecipe( Cookbook cookbook, Recipe recipe );
        Task RemoveCookbookRecipeAsync( Cookbook cookbook, Recipe recipe );

        void RemoveCommunityRecipe( CommunityRecipe communityRecipe );
        void RemoveCommunityRecipe( Community community, Recipe recipe );
        Task RemoveCommunityRecipeAsync( Community community, Recipe recipe );

        void RemoveRecipeCategory( RecipeCategory recipeCategory );
        void RemoveRecipeCategory( Recipe recipe, Category category );
        Task RemoveRecipeCategoryAsync( Recipe recipe, Category category );
    }
}
