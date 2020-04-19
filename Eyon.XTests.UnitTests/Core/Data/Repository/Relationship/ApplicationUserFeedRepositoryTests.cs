using System;
using Xunit;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class ApplicationUserFeedRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public ApplicationUserFeedRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(ApplicationUserFeedRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddApplicationUserFeed_FromEntities()
        {
            var firstEntity = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString()
            };
            var secondEntity = new Feed()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.ApplicationUserFeed.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.ApplicationUserId);
            Assert.Equal(secondEntity.Id, entityRelation.ObjectId);
        }
    }
}
