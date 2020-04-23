using Eyon.Core.Data.Repository.IRepository;
using Eyon.Core.DataCalls;
using Eyon.Core.DataCalls.IDataCall;
using Eyon.Core.Orchestrators;
using Eyon.Core.Orchestrators.IOrchestrator;
using Eyon.Models;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Eyon.XTests.UnitTests.Core.Orchestators
{
    public class FeedOrchestratorTests
    {
        public IFeedOrchestrator _feedOrchestrator;
        public IFeedDataCall _feedDataCall;
        public IUnitOfWork _unitOfWork;
        public List<Country> countries;
        public List<State> states;
        public List<Community> communities;
        public List<Cookbook> cookbooks;
        public List<Category> categories;
        public List<ApplicationUser> applicationUsers;
        public FeedOrchestratorTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(FeedOrchestratorTests));
            this._feedDataCall = new FeedDataCall(this._unitOfWork);
            this._feedOrchestrator = new FeedOrchestrator(this._unitOfWork, this._feedDataCall);
            countries = new List<Country>();
            states = new List<State>();
            communities = new List<Community>();
            cookbooks = new List<Cookbook>();
            categories = new List<Category>();
            applicationUsers = new List<ApplicationUser>();
            SampleData.SeedDatabase(this._unitOfWork, this.countries, this.states, this.communities, this.applicationUsers, this.cookbooks, this.categories);
        }

        public FeedItemViewModel GetFeedItemViewModel()
        {
            var feedItemViewModel = new FeedItemViewModel();
            feedItemViewModel.FeedItem = cookbooks[0];
            return feedItemViewModel;
        }

        public class AddAsync : FeedOrchestratorTests
        {
            [Fact]
            public async Task AddFeedItem_ResultNotNull()
            {
                string currentUserId = applicationUsers[0].Id;
                var feedItemViewModel = GetFeedItemViewModel();
                await _feedOrchestrator.AddAsync(currentUserId, feedItemViewModel);

                var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id);

                Assert.NotNull(feedFromDb);
            }

            [Fact]
            public async Task AddFeedItem_HasCategory_IdsAreEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                var feedItemViewModel = GetFeedItemViewModel();

                feedItemViewModel.Categories.Add(categories[0]);

                await _feedOrchestrator.AddAsync(currentUserId, feedItemViewModel);

                var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id, includeProperties: "FeedCategory");

                Assert.Equal(categories[0].Id, feedFromDb.FeedCategory.First().CategoryId);
            }
        }
    }
}
