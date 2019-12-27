using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.Relationship
{
    public class RecipeSiteImageRepository : Repository<RecipeSiteImage>, IRecipeSiteImageRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeSiteImageRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
