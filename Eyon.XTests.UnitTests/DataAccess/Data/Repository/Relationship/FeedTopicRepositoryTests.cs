using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class FeedTopicRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public FeedTopicRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(FeedTopicRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddFeedTopic_FromEntities()
        {
            var firstEntity = new Feed()
            {
                Id = 1
            };
            var secondEntity = new Topic()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.FeedTopic.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.FeedId);
            Assert.Equal(secondEntity.Id, entityRelation.TopicId);
        }
    }
}
