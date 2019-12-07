using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models.Relationship;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eyon.DataAccess.Data.Repository
{
    public class CookbookCategoriesRepository : Repository<CookbookCategories>, ICookbookCategoriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CookbookCategoriesRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add(CookbookCategories cookbookCategory)
        {
            if (_db.Category.Any(x => x.Id == cookbookCategory.CategoryId) == false)
                throw new Exception("Category does not exist in database.");

            if (_db.Cookbook.Any(x => x.Id == cookbookCategory.CookbookId) == false)
                throw new Exception("Cookbook does not exist in database.");

            if(_db.CookbookCategories.Any(x => x.CookbookId == cookbookCategory.CookbookId && x.CategoryId == cookbookCategory.CategoryId) == true)
                throw new Exception("Cookbook Category relationship already exists in the database.");

            base.Add(cookbookCategory);
        }
    }
}
