using Eyon.Models;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IRecipeRepository : IRepository<Models.Recipe>
    {        
        void Update(Recipe recipe);
    }    
}
