using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class CookbookRecipeRepository : Repository<CookbookRecipe>, ICookbookRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public CookbookRecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
