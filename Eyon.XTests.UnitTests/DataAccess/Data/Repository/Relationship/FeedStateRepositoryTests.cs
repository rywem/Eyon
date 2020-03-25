using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class FeedStateRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public FeedStateRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(FeedStateRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddFeedState_FromEntities()
        {
            var firstEntity = new Feed()
            {
                Id = 1
            };
            var secondEntity = new State()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.FeedState.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.FeedId);
            Assert.Equal(secondEntity.Id, entityRelation.StateId);
        }
    }
}
