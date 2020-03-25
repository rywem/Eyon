using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class FeedOrganizationRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public FeedOrganizationRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(FeedOrganizationRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddFeedOrganization_FromEntities()
        {
            var firstEntity = new Feed()
            {
                Id = 1
            };
            var secondEntity = new Organization()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.FeedOrganization.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.FeedId);
            Assert.Equal(secondEntity.Id, entityRelation.OrganizationId);
        }
    }
}
