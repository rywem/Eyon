using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System.Linq;

namespace Eyon.DataAccess.Data.Repository
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public void Update( Recipe recipe )
        {
            var objFromDb = _db.Recipe.FirstOrDefault(s => s.Id == recipe.Id);
            objFromDb.Name = recipe.Name;
            objFromDb.Cooktime = recipe.Cooktime;
            objFromDb.Instructions = recipe.Instructions;
            objFromDb.Name = recipe.Name;

            _db.SaveChanges();
        }
    }
}
