using Eyon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IIngredientRepository : IRepository<Models.Ingredient>
    {        
        void Update(Ingredient ingredient );
        //Task<bool> UpdateAsync( Ingredient ingredient );
    }    
}
