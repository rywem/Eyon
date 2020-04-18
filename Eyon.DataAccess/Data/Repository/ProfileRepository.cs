using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Eyon.DataAccess.Data.Repository
{
    public class ProfileRepository : PrivacyRepository<Profile, ApplicationUserProfile>, IProfileRepository
    {
        private readonly ApplicationDbContext _db;

        public ProfileRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
        public void UpdateIfOwner( string currentUserId, Profile profile )
        {
            var objFromDb = ( from r in _db.Profile
                              join a in _db.ApplicationUserProfile on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == profile.Id
                              select r ).FirstOrDefault();

            throw new NotImplementedException();
        }
    }
}
