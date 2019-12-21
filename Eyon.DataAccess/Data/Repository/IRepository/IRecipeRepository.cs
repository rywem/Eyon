using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IRecipeRepository : IRepository<Models.Recipe>, IOwnerRepository<Recipe, ApplicationUserRecipe>
    {     
        
    }    
}
