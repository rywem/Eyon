﻿using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IApplicationUserRecipeRepository : IRepository<ApplicationUserRecipe>, IManyToManyRelationshipRepository<ApplicationUserRecipe, ApplicationUser, Recipe>
    {


    }    
}
