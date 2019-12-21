using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Eyon.XTests.IntegrationsTests.DataAccess.Data.Repository
{
    public class Recipe_ApplicationUserTests : IDisposable
    {
        IUnitOfWork _unitOfWork;

        public Recipe_ApplicationUserTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(Recipe_ApplicationUserTests));
        }

        [Fact]
        public void GetRecipe_OwnedBy_OneRecordReturned()
        {
            var ownedEntity = new Recipe()
            {
                Name = "Ryan's cookies",
                Cooktime = "20 minutes"
            };

            var notOwnedEntity = new Recipe()
            {
                Name = "Ryan's cookies",
                Cooktime = "20 minutes"
            };
            _unitOfWork.Recipe.Add(ownedEntity);
            _unitOfWork.Recipe.Add(notOwnedEntity);
            _unitOfWork.Save();

            string userId = Guid.NewGuid().ToString();
            var owner = new ApplicationUser()
            {
                Email = "ryan.wemmer@gmail.com",
                FirstName = "Ryan",
                LastName = "Wemmer",
                Id = userId
            };

            _unitOfWork.ApplicationUser.Add(owner);
            _unitOfWork.Save();

            var applicationUserRecipe = new ApplicationUserRecipe()
            {
                ObjectId = ownedEntity.Id,
                ApplicationUserId = userId
            };

            _unitOfWork.ApplicationUserRecipe.Add(applicationUserRecipe);
            _unitOfWork.Save();
            
            var objFromDb = _unitOfWork.Recipe.GetAllOwned(userId);

            Assert.Single(objFromDb);
        }

        [Fact]
        public void TryGetRecipe_NotOwnedBy_NoRecordsReturned()
        {
            var ownedEntity = new Recipe()
            {
                Name = "Ryan's cookies",
                Cooktime = "20 minutes"
            };

            var notOwnedEntity = new Recipe()
            {
                Name = "Ryan's cookies",
                Cooktime = "20 minutes"
            };
            _unitOfWork.Recipe.Add(ownedEntity);
            _unitOfWork.Recipe.Add(notOwnedEntity);
            _unitOfWork.Save();

            string userId = Guid.NewGuid().ToString();
            var owner = new ApplicationUser()
            {
                Email = "ryan.wemmer@gmail.com",
                FirstName = "Ryan",
                LastName = "Wemmer",
                Id = userId
            };

            _unitOfWork.ApplicationUser.Add(owner);
            _unitOfWork.Save();

            var applicationUserRecipe = new ApplicationUserRecipe()
            {
                ObjectId = ownedEntity.Id,
                ApplicationUserId = userId
            };

            _unitOfWork.ApplicationUserRecipe.Add(applicationUserRecipe);
            _unitOfWork.Save();

            string newUserId = Guid.NewGuid().ToString();
            var objFromDb = _unitOfWork.Recipe.GetAllOwned(newUserId);

            Assert.Empty(objFromDb);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
