using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedCountryRepository : IRepository<FeedCountry>, IManyToManyRelationshipRepository<FeedCountry, Feed, Country>
    {        
    }    
}
