using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICookbookRepository : IRepository<Cookbook>, IPrivacyRepository<Cookbook, ApplicationUserCookbook>
    {
        void UpdateIfOwner( string currentUserId, Cookbook cookbook );
    }    
}
