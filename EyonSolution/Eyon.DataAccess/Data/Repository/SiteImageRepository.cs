using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class SiteImageRepository : Repository<SiteImage>, ISiteImageRepository
    {
        private readonly ApplicationDbContext _db;

        public SiteImageRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public SiteImage Get(long id)
        {
            return base.Get(id);
        }
        public void Update(SiteImage siteImage)
        {
            var objFromDb = _db.SiteImage.FirstOrDefault(s => s.Id == siteImage.Id);

            objFromDb.Encoded = siteImage.Encoded;
            objFromDb.FileType = siteImage.FileType;
            objFromDb.Title = siteImage.Title;
            _db.SaveChanges();
        }
    }    
}
