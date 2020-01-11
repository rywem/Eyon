using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;


namespace Eyon.DataAccess.Data.Repository
{
    public class CookbookCategoryRepository : Repository<CookbookCategories>, ICookbookCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CookbookCategoryRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add(CookbookCategories cookbookCategory)
        {
            if (_db.Category.Any(x => x.Id == cookbookCategory.CategoryId) == false)
                throw new Exception("Category does not exist in database.");

            if (_db.Cookbook.Any(x => x.Id == cookbookCategory.CookbookId) == false)
                throw new Exception("Cookbook does not exist in database.");

            if(_db.CookbookCategory.Any(x => x.CookbookId == cookbookCategory.CookbookId && x.CategoryId == cookbookCategory.CategoryId) == true)
                throw new Exception("Cookbook Category relationship already exists in the database.");

            base.Add(cookbookCategory);
        }
    }
}
