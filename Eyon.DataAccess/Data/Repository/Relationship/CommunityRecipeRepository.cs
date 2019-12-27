using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;


namespace Eyon.DataAccess.Data.Repository.Relationship
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
