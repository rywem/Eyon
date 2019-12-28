using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository
{
    public class RecipeRepository : OwnerRepository<Recipe, ApplicationUserRecipe>, IRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public bool UpdateIfOwner( string currentUserId, Recipe recipe )
        {
            var objFromDb = ( from r in _db.Recipe
                              join a in _db.ApplicationUserRecipes on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == recipe.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                return false;

            objFromDb.Name = recipe.Name;
            objFromDb.Cooktime = recipe.Cooktime;
            _db.SaveChanges();

            return true;
        }

        public async Task<bool> UpdateIfOwnerAsync( string currentUserId, Recipe recipe)
        {
            var objFromDb = ( from r in _db.Recipe
                              join a in _db.ApplicationUserRecipes on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == recipe.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                return false;

            objFromDb.Name = recipe.Name;
            objFromDb.Cooktime = recipe.Cooktime;
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
