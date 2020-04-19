using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IFeedCountryRepository : IRepository<FeedCountry>, IManyToManyRelationshipRepository<FeedCountry, Feed, Country>
    {        
    }    
}
