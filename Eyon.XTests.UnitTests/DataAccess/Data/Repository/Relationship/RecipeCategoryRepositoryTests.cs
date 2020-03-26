using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class RecipeCategoryRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public RecipeCategoryRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(RecipeCategoryRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddRecipeCategory_FromEntities()
        {
            var firstEntity = new Recipe()
            {
                Id = 1
            };
            var secondEntity = new Category()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.RecipeCategory.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.RecipeId);
            Assert.Equal(secondEntity.Id, entityRelation.CategoryId);
        }
    }
}
