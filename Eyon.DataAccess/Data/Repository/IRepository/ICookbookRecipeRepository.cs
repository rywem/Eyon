using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICookbookRecipeRepository : IRepository<CookbookRecipe>, IManyToManyRelationshipRepository<CookbookRecipe, Cookbook, Recipe>
    {
    {        
    }    
}
