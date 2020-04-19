using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICommunityStateRepository : IRepository<CommunityState>, IManyToManyRelationshipRepository<CommunityState, Community, State>
    {
    }
}
