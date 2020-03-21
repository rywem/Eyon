using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICookbookCategoryRepository : IRepository<CookbookCategories>, IManyToManyRelationshipRepository<CookbookCategories, Cookbook, Category>
    {        
    }    
}
