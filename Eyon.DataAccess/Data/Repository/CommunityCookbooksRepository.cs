using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class CommunityCookbooksRepository : Repository<CommunityCookbook>, ICommunityCookbooksRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityCookbooksRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
    }
}
