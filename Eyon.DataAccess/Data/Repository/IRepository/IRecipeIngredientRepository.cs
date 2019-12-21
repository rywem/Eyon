using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IRecipeIngredientRepository : IRepository<RecipeIngredient>
    {
        void Update( RecipeIngredient recipeIngredient );
    }    
}
