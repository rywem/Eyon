using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System.Linq;

namespace Eyon.DataAccess.Data.Repository
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly ApplicationDbContext _db;

        public IngredientRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public void Update( Ingredient ingredient )
        {
            var objFromDb = _db.Ingredient.FirstOrDefault(s => s.Id == ingredient.Id);
            objFromDb.Name = ingredient.Name;
            _db.SaveChanges();
        }
    }
}
