using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICommunityPostalCodeRepository : IRepository<CommunityPostalCode>, IManyToManyRelationshipRepository<CommunityPostalCode, Community, PostalCode>
    {        
    }    
}
