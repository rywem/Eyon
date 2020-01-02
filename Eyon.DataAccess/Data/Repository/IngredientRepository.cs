﻿using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
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

            if ( objFromDb == null )
                throw new WebUserSafeException("An error occurred.");
            objFromDb.Number = ingredient.Number;
            objFromDb.Text = ingredient.Text;
            dbSet.Update(objFromDb);
        }
    }
}
