using Xunit;
using System;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class ApplicationUserOrganizationRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public ApplicationUserOrganizationRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(ApplicationUserCookbookRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddApplicationUserOrganization_FromEntities()
        {
            var firstEntity = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString()
            };
            var secondEntity = new Organization()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.ApplicationUserOrganization.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.ApplicationUserId);
            Assert.Equal(secondEntity.Id, entityRelation.ObjectId);
        }
    }
}
