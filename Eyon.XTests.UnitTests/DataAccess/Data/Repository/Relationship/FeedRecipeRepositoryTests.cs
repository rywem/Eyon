using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class FeedRecipeRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public FeedRecipeRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(FeedRecipeRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddFeedRecipe_FromEntities()
        {
            var firstEntity = new Feed()
            {
                Id = 1
            };
            var secondEntity = new Recipe()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.FeedRecipe.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.FeedId);
            Assert.Equal(secondEntity.Id, entityRelation.RecipeId);
        }
    }
}
