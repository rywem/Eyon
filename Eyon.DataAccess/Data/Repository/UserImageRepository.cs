using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository
{
    public class UserImageRepository : OwnerRepository<UserImage, ApplicationUserUserImage>, IUserImageRepository
    {
        private readonly ApplicationDbContext _db;

        public UserImageRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( UserImage entity )
        {
            entity.CreationDateTime = DateTime.Now.ToUniversalTime();
            base.Add(entity);
        }

        public void UpdateIfOwner( string currentUserId, UserImage userImage )
        {
            var objFromDb = ( from r in _db.UserImage
                              join a in _db.ApplicationUserRecipe on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == userImage.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                if ( objFromDb == null )
                    throw new SafeException("An error ocurred.", new Exception(string.Format("Ownership relationship not found on record. currentUserId {0},  userImage.Id {1}", currentUserId, userImage.Id)));

            objFromDb.Description = userImage.Description;
            dbSet.Update(objFromDb);            
        }
    }    
}
