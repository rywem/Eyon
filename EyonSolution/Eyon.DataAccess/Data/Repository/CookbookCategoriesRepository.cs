using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class CookbookCategoriesRepository : Repository<CookbookCategories>, ICookbookCategoriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CookbookCategoriesRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
    }
}
