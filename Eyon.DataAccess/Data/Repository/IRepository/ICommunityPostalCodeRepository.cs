﻿using Eyon.Models;
using Eyon.Models.Relationship;
namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICommunityPostalCodeRepository : IRepository<CommunityPostalCode>, IManyToManyRelationshipRepository<CommunityPostalCode, Community, PostalCode>
    {        
    }    
}
