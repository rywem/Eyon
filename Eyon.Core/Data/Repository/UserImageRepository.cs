using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository
{
    public class UserImageRepository : PrivacyRepository<UserImage, ApplicationUserUserImage>, IUserImageRepository
    {
        private readonly ApplicationDbContext _db;

        public UserImageRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( UserImage entity )
        {
            entity.CreationDateTime = DateTime.UtcNow;
            entity.ModifiedDateTime = entity.CreationDateTime;
            base.Add(entity);
        }

        public void UpdateIfOwner( string currentUserId, UserImage userImage )
        {
            var objFromDb = ( from r in _db.UserImage
                              join a in _db.ApplicationUserRecipe on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == userImage.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                throw new SafeException("An error ocurred.", new Exception(string.Format("Ownership relationship not found on record. currentUserId {0},  userImage.Id {1}", currentUserId, userImage.Id)));

            objFromDb.Description = userImage.Description;
            userImage.ModifiedDateTime = objFromDb.ModifiedDateTime = DateTime.UtcNow;
            objFromDb.Description = userImage.Description;
            objFromDb.Privacy = userImage.Privacy;
            objFromDb.FileName = userImage.FileName;
            objFromDb.FileNameThumb = userImage.FileNameThumb;
            objFromDb.FileType = userImage.FileType;
            
            //userImage.ModifiedDateTime = objFromDb.ModifiedDateTime; // Write back to parameter object the correct time stamp.
            dbSet.Update(objFromDb);            
        }
    }    
}
