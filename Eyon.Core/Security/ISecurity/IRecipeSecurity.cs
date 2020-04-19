using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.Core.Security.ISecurity
{
    public interface IRecipeSecurity
    {
        Task<RecipeViewModel> GetAsync( string currentApplicationUserId, long id );
        Task UpdateAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel );
        Task AddAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel );
        Task DeleteAsync( string currentApplicationUserId, long id );
    }
}
