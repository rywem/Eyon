using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;


namespace Eyon.DataAccess.Data.Repository
{
    public class RecipeCategoryRepository : ManyToManyRelationshipRepository<RecipeCategory, Recipe, Category>, IRecipeCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeCategoryRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
        
        public override RecipeCategory AddFromEntities( Recipe firstEntity, Category secondEntity )
        {
            RecipeCategory recipeCategory = new RecipeCategory()
            {
               RecipeId = firstEntity.Id,
               CategoryId = secondEntity.Id
            };

            base.Add(recipeCategory);
            return recipeCategory;

        }
    }
}
