using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System.Linq;
using System.Threading.Tasks;

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
            objFromDb.Text = ingredient.Text;
            _db.SaveChanges();
        }

        public async Task<bool> UpdateAsync( Ingredient ingredient )
        {
            var objFromDb = _db.Ingredient.FirstOrDefault(s => s.Id == ingredient.Id);

            if ( objFromDb == null )
                return false;
            objFromDb.Number = ingredient.Number;
            objFromDb.Text = ingredient.Text;

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
