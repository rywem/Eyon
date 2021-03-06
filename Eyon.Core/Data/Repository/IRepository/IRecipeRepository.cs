﻿using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IRecipeRepository : IRepository<Models.Recipe>, IPrivacyRepository<Recipe, ApplicationUserRecipe>
    {
        void UpdateIfOwner( string currentUserId, Recipe recipe );
        //Task<bool> UpdateIfOwnerAsync( string currentUserId, Recipe recipe );
    }    
}
