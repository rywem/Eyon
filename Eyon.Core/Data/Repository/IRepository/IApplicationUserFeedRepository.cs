using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IApplicationUserFeedRepository : IRepository<ApplicationUserFeed>, IManyToManyRelationshipRepository<ApplicationUserFeed, ApplicationUser, Feed>
    {


    }    
}
