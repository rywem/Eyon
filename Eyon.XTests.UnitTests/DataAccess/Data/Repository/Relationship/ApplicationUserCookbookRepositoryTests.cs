using Xunit;
using System;
using Eyon.DataAccess.Data.Repository;
using Eyon.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using System.Text;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
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
            Guid guid = new Guid();

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
