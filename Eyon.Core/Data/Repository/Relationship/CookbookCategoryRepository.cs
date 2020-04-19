using Eyon.Core.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models;

namespace Eyon.Core.Data.Repository
{
    public class CookbookCategoryRepository : Repository<CookbookCategory>, ICookbookCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CookbookCategoryRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add(CookbookCategory cookbookCategory)
        {
            if (_db.Category.Any(x => x.Id == cookbookCategory.CategoryId) == false)
                throw new Exception("Category does not exist in database.");

            if (_db.Cookbook.Any(x => x.Id == cookbookCategory.CookbookId) == false)
                throw new Exception("Cookbook does not exist in database.");

            if(_db.CookbookCategory.Any(x => x.CookbookId == cookbookCategory.CookbookId && x.CategoryId == cookbookCategory.CategoryId) == true)
                throw new Exception("Cookbook Category relationship already exists in the database.");

            base.Add(cookbookCategory);
        }

        public CookbookCategory AddFromEntities( Cookbook firstEntity, Category secondEntity )
        {
            var newObj = new CookbookCategory()
            {
                CookbookId = firstEntity.Id,
                CategoryId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
