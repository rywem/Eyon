using System;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Xunit;

namespace Eyon.XTests.UnitTests.Core.Data.Repository
{
    public class ApplicationUserRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public ApplicationUserRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(ApplicationUserRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void LockUser_AssertUserLocked_LockTimeInDistantFuture()
        {
            string id = Guid.NewGuid().ToString();
            ApplicationUser user = new ApplicationUser()
            {
                Id = id,
            };

            _unitOfWork.ApplicationUser.Add(user);
            _unitOfWork.Save();

            _unitOfWork.ApplicationUser.LockUser(id);

            var entityFromDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == id);            
            Assert.True(entityFromDb.LockoutEnd > DateTime.Now.ToUniversalTime().AddYears(50));
        }

        [Fact]
        public void UnlockUser_AssertUserLocked_LockTimeIsNow()
        {
            string id = Guid.NewGuid().ToString();
            ApplicationUser user = new ApplicationUser()
            {
                Id = id,
            };

            _unitOfWork.ApplicationUser.Add(user);
            _unitOfWork.Save();

            _unitOfWork.ApplicationUser.LockUser(id);
            _unitOfWork.ApplicationUser.UnlockUser(id);

            var entityFromDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == id);

            Assert.True(entityFromDb.LockoutEnd <= DateTime.Now.ToUniversalTime());
        }
    }
}
