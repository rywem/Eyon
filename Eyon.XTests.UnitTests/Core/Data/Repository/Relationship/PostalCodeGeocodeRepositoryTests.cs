using System;
using Xunit;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class PostalCodeGeocodeRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public PostalCodeGeocodeRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(PostalCodeGeocodeRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddPostalCodeGeocode_FromEntities()
        {
            var firstEntity = new PostalCode()
            {
                Id = 1
            };
            var secondEntity = new Geocode()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.PostalCodeGeocode.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.PostalCodeId);
            Assert.Equal(secondEntity.Id, entityRelation.GeocodeId);
        }
    }
}
