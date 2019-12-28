using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eyon.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.Category.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public IEnumerable<Category> Search(string searchString, string includeProperties = null)
        {
            IQueryable<Category> query = _db.Category.Where(x => x.Name.Contains(searchString));
                        
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }            

            return query.ToList();            
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Category.FirstOrDefault(s => s.Id == category.Id);
            objFromDb.Name = category.Name;
            objFromDb.DisplayOrder = category.DisplayOrder;            
            objFromDb.SiteImageId = category.SiteImageId;
            _db.SaveChanges(); //I think this shouldn't be called here.
        }
        //public void Update( Category category )
        //{
        //    var objFromDb = _db.Category.FirstOrDefault(s => s.Id == category.Id);
        //    objFromDb.Name = category.Name;
        //    objFromDb.DisplayOrder = category.DisplayOrder;
        //    objFromDb.SiteImageId = category.SiteImageId;
        //    _db.SaveChanges();
        //}
    }
}
