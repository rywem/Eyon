﻿using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICookbookCategoryRepository : IRepository<CookbookCategory>, IManyToManyRelationshipRepository<CookbookCategory, Cookbook, Category>
    {        
    }    
}
