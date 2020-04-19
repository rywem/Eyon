using System;
using Xunit;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class ApplicationUserUserImageRepositoryTests
    {
        IUnitOfWork _unitOfWork;
        public ApplicationUserUserImageRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(ApplicationUserUserImageRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddApplicationUserUserImage_FromEntities()
        {
            var firstEntity = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString()
            };
            var secondEntity = new UserImage()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.ApplicationUserUserImage.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.ApplicationUserId);
            Assert.Equal(secondEntity.Id, entityRelation.ObjectId);
        }
    }
}
