using Eyon.Models;
using Eyon.Models.Relationship;
using System;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IProfileRepository : IRepository<Models.Profile>, IPrivacyRepository<Profile, ApplicationUserProfile>
    {
        void UpdateIfOwner( string currentUserId, Profile profile );
    }
}
