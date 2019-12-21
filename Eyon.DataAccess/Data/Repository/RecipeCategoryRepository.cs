using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;


namespace Eyon.DataAccess.Data.Repository
{
    public class RecipeCategoryRepository : Repository<RecipeCategory>, IRecipeCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeCategoryRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
