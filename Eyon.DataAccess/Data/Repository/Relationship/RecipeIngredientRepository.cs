using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;
using System.Linq;

namespace Eyon.DataAccess.Data.Repository.Relationship
{
    public class RecipeIngredientRepository : Repository<RecipeIngredient>, IRecipeIngredientRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeIngredientRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public void Update( RecipeIngredient recipeIngredient )
        {
            var objFromDb = _db.RecipeIngredients.FirstOrDefault(s => s.RecipeId == recipeIngredient.RecipeId && s.IngredientId == recipeIngredient.IngredientId);
            objFromDb.Measure = recipeIngredient.Measure;
            _db.SaveChanges();
        }
    }
}
