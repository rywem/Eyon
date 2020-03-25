using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class CommunityRecipeRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public CommunityRecipeRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CommunityRecipeRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddCommunityRecipe_FromEntities()
        {
            var firstEntity = new Community()
            {
                Id = 1
            };
            var secondEntity = new Recipe()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.CommunityRecipe.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.CommunityId);
            Assert.Equal(secondEntity.Id, entityRelation.RecipeId);
        }
    }
}
