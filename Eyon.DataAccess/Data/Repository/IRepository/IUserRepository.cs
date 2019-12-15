using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        bool ChangeRole( ApplicationUser userToChange, string newRole );

        void LockUser( string userId );
        void UnlockUser( string userId );

    }
}
