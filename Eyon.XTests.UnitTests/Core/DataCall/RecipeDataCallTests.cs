using Eyon.Core.Data.Repository.IRepository;
using Eyon.Core.DataCalls;
using Eyon.Core.DataCalls.IDataCall;
using Eyon.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Eyon.XTests.UnitTests.Core.DataCall
{
    public class RecipeDataCallTests
    {
        private RecipeDataCall _recipeDataCall;
        private IUnitOfWork _unitOfWork;
        public RecipeDataCallTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(RecipeDataCallTests));
            this._recipeDataCall = new RecipeDataCall(_unitOfWork);
        }
        [Fact]
        public async Task AddRecipeWithRelationship_Test()
        {
            Recipe recipe = new Recipe()
            {
                Name = "Macaroni and Cheese",
                Description = "A cheesy dish.",
                Cooktime = "10 minutes",
                PrepTime = "10 minutes",
                Servings = "2-4",
                Privacy = Models.Enums.Privacy.Public
            };

            string userId = Guid.NewGuid().ToString();
            var applicationUser = new ApplicationUser()
            {
                Id = userId
            };
            _unitOfWork.ApplicationUser.Add(applicationUser);
            await _unitOfWork.SaveAsync();
            await _recipeDataCall.AddRecipeWithRelationship(userId, recipe, true);

            var recipeFromDb = _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(userId, x => x.Id == recipe.Id);
            var recipeOwnerRelationship = _unitOfWork.ApplicationUserRecipe.GetFirstOrDefaultAsync(x => x.ApplicationUserId == userId);
            Assert.NotNull(recipeFromDb);
            Assert.NotNull(recipeOwnerRelationship);
        }
    }
}
