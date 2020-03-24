using Xunit;
using System;
using Eyon.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using System.Text;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class ApplicationUserFeedRepositoryTests
    {
        IUnitOfWork _unitOfWork;
        public ApplicationUserFeedRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(ApplicationUserFeedRepositoryTests));
        }

        [Fact]
        public void AddApplicationUserFeed_FromEntities()
        {
            var firstEntity = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString()
            };
            var secondEntity = new Feed()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.ApplicationUserFeed.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.ApplicationUserId);
            Assert.Equal(secondEntity.Id, entityRelation.ObjectId);
        }
    }
}
