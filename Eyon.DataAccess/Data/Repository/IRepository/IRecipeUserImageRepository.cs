using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IRecipeUserImageRepository : IRepository<RecipeUserImage>, IManyToManyRelationshipRepository<RecipeUserImage, Recipe, UserImage>
    {
    }
}
