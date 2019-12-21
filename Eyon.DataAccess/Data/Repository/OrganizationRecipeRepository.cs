using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class OrganizationRecipeRepository : Repository<OrganizationRecipe>, IOrganizationRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public OrganizationRecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

    }
}
