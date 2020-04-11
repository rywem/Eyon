using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.DataAccess.DataCalls.IDataCall;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.DataCalls
{
    public class RecipeDataCall : IRecipeDataCall
    {
        private IUnitOfWork _unitOfWork;

        public RecipeDataCall(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task AddRecipeWithRelationship( string currentApplicationUserId, Recipe recipe, bool saveOnRelationshipInsert = false )
        {
            _unitOfWork.Recipe.Add(recipe);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Recipe.AddOwnerRelationship(currentApplicationUserId, recipe, new ApplicationUserRecipe());

            if ( saveOnRelationshipInsert == true )
                await _unitOfWork.SaveAsync();
        }
    }
}
