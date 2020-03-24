using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class CommunityGeocodeRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public CommunityGeocodeRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CommunityGeocodeRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddCommunityCookbook_FromEntities()
        {
            var firstEntity = new Community()
            {
                Id = 1
            };
            var secondEntity = new Geocode()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.CommunityGeocode.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.CommunityId);
            Assert.Equal(secondEntity.Id, entityRelation.GeocodeId);
        }
    }
}
