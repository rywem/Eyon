using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class RecipeUserImageRepository : Repository<RecipeUserImage>, IRecipeUserImageRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeUserImageRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
