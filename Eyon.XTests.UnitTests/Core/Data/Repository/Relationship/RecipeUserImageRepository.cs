using System;
using Xunit;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class RecipeUserImageRepository : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public RecipeUserImageRepository()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(RecipeUserImageRepository));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddRecipeUserImage_FromEntities()
        {
            var firstEntity = new Recipe()
            {
                Id = 1
            };
            var secondEntity = new UserImage()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.RecipeUserImage.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.RecipeId);
            Assert.Equal(secondEntity.Id, entityRelation.UserImageId);
        }
    }
}
