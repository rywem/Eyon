using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using System.Linq;

namespace Eyon.DataAccess.Data.Repository
{
    public class CommunityRecipeRepository : Repository<CommunityRecipe>, ICommunityRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityRecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public CommunityRecipe AddFromEntities( Community firstEntity, Recipe secondEntity )
        {
            var newObj = new CommunityRecipe()
            {
                CommunityId = firstEntity.Id,
                RecipeId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
