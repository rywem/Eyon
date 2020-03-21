using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class CookbookRecipeRepository : ManyToManyRelationshipRepository<CookbookRecipe, Cookbook, Recipe>, ICookbookRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public CookbookRecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public override CookbookRecipe AddFromEntities( Cookbook firstEntity, Recipe secondEntity )
        {
            CookbookRecipe cookbookRecipe = new CookbookRecipe()
            {
                CookbookId = firstEntity.Id,
                RecipeId = secondEntity.Id
            };
            base.Add(cookbookRecipe);
            return cookbookRecipe;
        }
    }
}
