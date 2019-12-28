using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
        public bool ChangeRole( ApplicationUser userToChange, string newRole )
        {
            throw new NotImplementedException();
        }

        public void LockUser( string userId )
        {
            var userFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            userFromDb.LockoutEnd = DateTime.Now.AddYears(1000).ToUniversalTime();
            _db.SaveChanges();
        }

        public void UnlockUser( string userId )
        {
            var userFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            userFromDb.LockoutEnd = DateTime.Now.ToUniversalTime();
            _db.SaveChanges();
        }
    }
}

