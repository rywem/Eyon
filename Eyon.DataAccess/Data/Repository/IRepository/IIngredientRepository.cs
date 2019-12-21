using Eyon.Models;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IIngredientRepository : IRepository<Models.Ingredient>
    {        
        void Update(Ingredient ingredient );
    }    
}
