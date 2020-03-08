using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;

namespace Eyon.DataAccess.Data.Repository
{
    public class FeedRecipeRepository : Repository<FeedRecipe>, IFeedRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedRecipeRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedRecipe entityToAdd )
        {
            if (_db.FeedRecipe.Any(x => x.FeedId == entityToAdd.FeedId && x.RecipeId == entityToAdd.RecipeId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  RecipeId {1}", entityToAdd.FeedId, entityToAdd.RecipeId)));

            base.Add(entityToAdd);
        }
    }
}
