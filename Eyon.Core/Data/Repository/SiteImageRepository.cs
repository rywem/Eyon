using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using System;
using System.Linq;

namespace Eyon.Core.Data.Repository
{
    public class SiteImageRepository : Repository<SiteImage>, ISiteImageRepository
    {
        private readonly ApplicationDbContext _db;

        public SiteImageRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public void Update(SiteImage siteImage)
        {
            var objFromDb = _db.SiteImage.FirstOrDefault(s => s.Id == siteImage.Id);

            if ( objFromDb == null )
                throw new SafeException(Models.Enums.ErrorType.AnErrorOccurred, new Exception(string.Format("SiteImage not found, Id {0} , FileName {1}", siteImage.Id, siteImage.FileName)));

            objFromDb.FileName = siteImage.FileName;
            objFromDb.FileNameThumb = siteImage.FileNameThumb;
            objFromDb.FileType = siteImage.FileType;
            objFromDb.Description = siteImage.Description;
            objFromDb.Alt = siteImage.Alt;
            dbSet.Update(objFromDb);
        }
    }    
}
