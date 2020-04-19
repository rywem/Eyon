using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IRecipeUserImageRepository : IRepository<RecipeUserImage>, IManyToManyRelationshipRepository<RecipeUserImage, Recipe, UserImage>
    {
    }
}
