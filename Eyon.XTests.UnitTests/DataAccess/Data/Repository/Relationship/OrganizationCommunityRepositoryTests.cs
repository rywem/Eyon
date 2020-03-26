using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class OrganizationCommunityRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public OrganizationCommunityRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(OrganizationCommunityRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddOrganizationCommunity_FromEntities()
        {
            var firstEntity = new Organization()
            {
                Id = 1
            };
            var secondEntity = new Community()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.OrganizationCommunity.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.OrganizationId);
            Assert.Equal(secondEntity.Id, entityRelation.CommunityId);
        }

    }
}
