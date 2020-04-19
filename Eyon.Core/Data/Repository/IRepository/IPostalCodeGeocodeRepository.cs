using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IPostalCodeGeocodeRepository : IRepository<PostalCodeGeocode>, IManyToManyRelationshipRepository<PostalCodeGeocode, PostalCode, Geocode>
    {        
    }    
}
