using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class ApplicationUserRecipeRepository : Repository<ApplicationUserRecipe>, IApplicationUserRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public ApplicationUserRecipe AddFromEntities( ApplicationUser firstEntity, Recipe secondEntity )
        {
            var newObj = new ApplicationUserRecipe()
            {
                ApplicationUserId = firstEntity.Id,
                ObjectId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
