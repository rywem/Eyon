using System;
using Xunit;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class ApplicationUserRecipeRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public ApplicationUserRecipeRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(ApplicationUserRecipeRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddApplicationUserRecipe_FromEntities()
        {
            var firstEntity = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString()
            };
            var secondEntity = new Recipe()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.ApplicationUserRecipe.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.ApplicationUserId);
            Assert.Equal(secondEntity.Id, entityRelation.ObjectId);
        }
    }
}
