using System;
using Xunit;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class CommunityWebReferenceRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public CommunityWebReferenceRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CommunityWebReferenceRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddCommunityWebReference_FromEntities()
        {
            var firstEntity = new Community()
            {
                Id = 1
            };
            var secondEntity = new WebReference()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.CommunityWebReference.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.CommunityId);
            Assert.Equal(secondEntity.Id, entityRelation.WebReferenceId);
        }
    }
}
