using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IRecipeCategoryRepository : IRepository<RecipeCategory>, IManyToManyRelationshipRepository<RecipeCategory, Recipe, Category>
    {
    }
}
