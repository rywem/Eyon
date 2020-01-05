using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IUserImageRepository : IRepository<Models.UserImage>, IOwnerRepository<UserImage, ApplicationUserUserImage>
    {        
        void UpdateIfOwner( string currentUserId, UserImage recipe );
        //Task<bool> UpdateIfOwnerAsync( string currentUserId, UserImage recipe );
    }
}
