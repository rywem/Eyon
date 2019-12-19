using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System.Linq;

namespace Eyon.DataAccess.Data.Repository
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        private readonly ApplicationDbContext _db;

        public OrganizationRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public void Update( Organization organization )
        {
            var objFromDb = _db.Organization.FirstOrDefault(s => s.Id == organization.Id);
            objFromDb.Name = organization.Name;
            objFromDb.Description = organization.Description;
            objFromDb.Type = organization.Type;
            objFromDb.Website = organization.Website;            
            _db.SaveChanges();
        }


    }
}
