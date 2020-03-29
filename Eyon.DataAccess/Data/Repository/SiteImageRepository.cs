using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System.Linq;

namespace Eyon.DataAccess.Data.Repository
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

            objFromDb.FileName = siteImage.FileName;
            objFromDb.FileNameThumb = siteImage.FileNameThumb;
            objFromDb.FileType = siteImage.FileType;
            objFromDb.Description = siteImage.Description;
            objFromDb.Alt = siteImage.Alt;
            dbSet.Update(objFromDb);
        }
    }    
}
