﻿using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICommunityGeocodeRepository : IRepository<CommunityGeocode>, IManyToManyRelationshipRepository<CommunityGeocode, Community, Geocode>
    {        
    }    
}
