using Xunit;
using System;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data.Repository.Relationship
{
    public class ApplicationUserCookbookRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public ApplicationUserCookbookRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(ApplicationUserCookbookRepositoryTests));
        }

        [Fact]
        public void AddApplicationUserCookbook_FromEntities()
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString()
            };
            Cookbook cookbook = new Cookbook()
            {
                Id = 1
            };

            var entity = _unitOfWork.ApplicationUserCookbook.AddFromEntities(applicationUser, cookbook);
            Assert.Equal(applicationUser.Id, entity.ApplicationUserId);
            Assert.Equal(cookbook.Id, entity.ObjectId);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
