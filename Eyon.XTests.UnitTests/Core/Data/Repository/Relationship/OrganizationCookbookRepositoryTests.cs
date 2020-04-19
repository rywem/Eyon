using System;
using Xunit;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class OrganizationCookbookRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public OrganizationCookbookRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(OrganizationCookbookRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddOrganizationCookbook_FromEntities()
        {
            var firstEntity = new Organization()
            {
                Id = 1
            };
            var secondEntity = new Cookbook()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.OrganizationCookbook.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.OrganizationId);
            Assert.Equal(secondEntity.Id, entityRelation.CookbookId);
        }
    }
}
