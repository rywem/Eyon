using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eyon.DataAccess.Data.Repository
{
    public class RecipeRepository : OwnerRepository<Recipe, ApplicationUserRecipe>, IRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        //public IEnumerable<Recipe> GetAllOwned( string ownerId, Expression<Func<Recipe, bool>> filter = null, Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>> orderBy = null, string includeProperties = null )
        //{
        //    IQueryable<Recipe> query = dbSet;

        //    query = from e in dbSet
        //            join k in Context.Set<ApplicationUserRecipe>() on e.Id equals k.RecipeId
        //            where k.ApplicationUserId.Equals(ownerId)
        //            select e;
            
        //    if ( filter != null )
        //    {
        //        query = query.Where(filter);
        //    }
        //    // include properties will be comma seperated
        //    if ( includeProperties != null )
        //    {
        //        foreach ( var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) )
        //        {
        //            query = query.Include(includeProperty);
        //        }
        //    }

        //    if ( orderBy != null )
        //    {
        //        return orderBy(query).ToList();
        //    }
            
        //    return query.ToList();
        //}

        //public void SafeUpdate( Recipe entity, string ownerId )
        //{
            
        //}

        //public void Update( Recipe recipe )
        //{
        //    var objFromDb = _db.Recipe.FirstOrDefault(s => s.Id == recipe.Id);
        //    objFromDb.Name = recipe.Name;
        //    objFromDb.Cooktime = recipe.Cooktime;
        //    objFromDb.Instructions = recipe.Instructions;
        //    objFromDb.Name = recipe.Name;

        //    _db.SaveChanges();
        //}
    }
}
