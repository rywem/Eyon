using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository
{
    public class CommunityCookbookRepository : Repository<CommunityCookbook>, ICommunityCookbookRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityCookbookRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public CommunityCookbook AddFromEntities( Community community, Cookbook cookbook )
        {
            var newObj = new CommunityCookbook()
            {
                CommunityId = community.Id,
                CookbookId = cookbook.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
