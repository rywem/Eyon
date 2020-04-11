using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.DataAccess.DataCalls;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Eyon.XTests.UnitTests.DataAccess.DataCall
{
    public class FeedDataCallTests
    {
        private FeedDataCall _feedDataCall;
        private IUnitOfWork _unitOfWork;
        public FeedDataCallTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(FeedDataCallTests));
            this._feedDataCall = new FeedDataCall(_unitOfWork);
        }
        [Fact]
        public async Task AddFeedItemWithRelationship_Test()
        {
            Recipe recipe = new Recipe()
            {
                Name = "Macaroni and Cheese",
                Description = "A cheesy dish.",
                Cooktime = "10 minutes",
                PrepTime = "10 minutes",
                Servings = "2-4",
                Privacy = Models.Enums.Privacy.Public
            };

            string userId = Guid.NewGuid().ToString();
            var applicationUser = new ApplicationUser()
            {
                Id = userId
            };
            _unitOfWork.ApplicationUser.Add(applicationUser);
            await _unitOfWork.SaveAsync();
            var feed = await _feedDataCall.AddFeedWithRelationship(userId, recipe, true);

            var feedFromDb = _unitOfWork.Feed.GetFirstOrDefaultOwnedAsync(userId, x => x.Id == feed.Id);
            var recipeOwnerRelationship = _unitOfWork.ApplicationUserFeed.GetFirstOrDefaultAsync(x => x.ApplicationUserId == userId);
            Assert.NotNull(feedFromDb);
            Assert.NotNull(recipeOwnerRelationship);
        }
    }
}
