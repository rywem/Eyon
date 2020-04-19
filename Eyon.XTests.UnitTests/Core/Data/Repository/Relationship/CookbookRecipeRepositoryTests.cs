using System;
using Xunit;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class CookbookRecipeRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public CookbookRecipeRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CookbookRecipeRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddCoookbookRecipe_FromEntities()
        {
            var firstEntity = new Cookbook()
            {
                Id = 1
            };
            var secondEntity = new Recipe()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.CookbookRecipe.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.CookbookId);
            Assert.Equal(secondEntity.Id, entityRelation.RecipeId);
        }

    }
}
