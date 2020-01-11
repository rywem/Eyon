using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICookbookRepository : IRepository<Cookbook>, IOwnerRepository<Cookbook, ApplicationUserCookbook>
    {
        void UpdateIfOwner( string currentUserId, Cookbook cookbook );
    }    
}
