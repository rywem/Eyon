using Eyon.Models;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.Core.Orchestrators.IOrchestrator
{
    public interface IRecipeOrchestrator
    {
        Task<RecipeViewModel> GetAsync( string currentApplicationUserId, long id );
        Task AddTransactionAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel );
        Task AddAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel );
        Task UpdateTransactionAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel );
        Task UpdateAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel );
        Task DeleteTransactionAsync( string currentApplicationUserId, long recipeId );
        Task DeleteAsync( string currentApplicationUserId, long recipeId );
    }
}
