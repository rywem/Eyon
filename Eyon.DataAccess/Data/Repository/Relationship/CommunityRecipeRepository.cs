using Eyon.DataAccess.Data.Repository.IRepository;
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
    }
}
