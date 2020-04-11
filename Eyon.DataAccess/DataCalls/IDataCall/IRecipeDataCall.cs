using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.DataCalls.IDataCall
{
    public interface IRecipeDataCall
    {
        Task AddRecipeWithRelationship( string currentApplicationUserId, Recipe recipe, bool saveOnRelationshipInsert = false );
    }
}
