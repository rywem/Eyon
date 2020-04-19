using Eyon.Models;
using Eyon.Models.Relationship;
using System.Collections.Generic;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICommunityRecipeRepository : IRepository<Models.Relationship.CommunityRecipe>, IManyToManyRelationshipRepository<CommunityRecipe, Community, Recipe>
    {        
    }    
}
