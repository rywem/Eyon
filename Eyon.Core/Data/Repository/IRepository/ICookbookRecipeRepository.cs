using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICookbookRecipeRepository : IRepository<CookbookRecipe>, IManyToManyRelationshipRepository<CookbookRecipe, Cookbook, Recipe>
    {            
    }    
}
