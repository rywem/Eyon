using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICommunityWebReferenceRepository : IRepository<CommunityWebReference>, IManyToManyRelationshipRepository<CommunityWebReference, Community, WebReference>
    {        
    }    
}
