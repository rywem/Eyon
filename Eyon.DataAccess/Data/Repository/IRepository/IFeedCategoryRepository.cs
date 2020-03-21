﻿using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IFeedCategoryRepository : IRepository<FeedCategory>, IManyToManyRelationshipRepository<FeedCategory, Feed, Category>
    {        
    }    
}
