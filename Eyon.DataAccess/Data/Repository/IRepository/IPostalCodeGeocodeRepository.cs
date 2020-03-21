using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IPostalCodeGeocodeRepository : IRepository<PostalCodeGeocode>, IManyToManyRelationshipRepository<PostalCodeGeocode, PostalCode, Geocode>
    {        
    }    
}
