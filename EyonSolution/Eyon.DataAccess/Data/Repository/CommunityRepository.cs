using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eyon.DataAccess.Data.Repository
{
    public class CommunityRepository : Repository<Community>, ICommunityRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityRepository(ApplicationDbContext db) : base(db)
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

        public void Update(Community community)
        {
            var objFromDb = _db.Community.FirstOrDefault(s => s.Id == community.Id);
            objFromDb.Name = community.Name;
            
            objFromDb.County = community.County;
            
            objFromDb.WikipediaURL = community.WikipediaURL;
            objFromDb.Active = community.Active;
            _db.SaveChanges();
        }
    }
}
