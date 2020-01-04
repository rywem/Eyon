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

            objFromDb.Encoded = siteImage.Encoded;
            objFromDb.FileType = siteImage.FileType;
            objFromDb.Title = siteImage.Title;
            objFromDb.Alt = siteImage.Alt;
            //_db.SaveChanges();
            dbSet.Update(objFromDb);
        }
    }    
}
