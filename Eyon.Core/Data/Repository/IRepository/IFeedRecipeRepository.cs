using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedRecipeRepository : IRepository<FeedRecipe>, IManyToManyRelationshipRepository<FeedRecipe, Feed, Recipe>
    {        
    }    
}
