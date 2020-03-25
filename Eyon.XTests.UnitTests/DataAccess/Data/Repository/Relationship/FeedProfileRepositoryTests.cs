using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class FeedProfileRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public FeedProfileRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(FeedProfileRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddFeedProfile_FromEntities()
        {
            var firstEntity = new Feed()
            {
                Id = 1
            };
            var secondEntity = new Profile()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.FeedProfile.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.FeedId);
            Assert.Equal(secondEntity.Id, entityRelation.ProfileId);
        }
    }
}
