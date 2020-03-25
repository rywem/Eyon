using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class CommunityStateRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public CommunityStateRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CommunityStateRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddCommunityState_FromEntities()
        {
            var firstEntity = new Community()
            {
                Id = 1
            };
            var secondEntity = new State()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.CommunityState.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.CommunityId);
            Assert.Equal(secondEntity.Id, entityRelation.StateId);
        }
    }
}
