using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class CommunityCookbookRepository : Repository<CommunityCookbook>, ICommunityCookbookRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityCookbookRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
    }
}
