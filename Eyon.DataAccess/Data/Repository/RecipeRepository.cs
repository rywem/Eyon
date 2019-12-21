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
    }
}
