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
        public List<Organization> organizations;
        public List<Profile> profiles;
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
            organizations = new List<Organization>();
            profiles = new List<Profile>();
            SampleData.SeedDatabase(this._unitOfWork, this.countries, this.states, this.communities, this.applicationUsers, this.cookbooks, this.categories, this.organizations, this.profiles);
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
                Assert.True(feedFromDb.Id > 0);
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
                Assert.True(feedFromDb.FeedCategory.First().CategoryId > 0);
            }
            [Fact]
            public async Task AddFeedItem_HasCommunity_IdsAreEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                var feedItemViewModel = GetFeedItemViewModel();

                feedItemViewModel.Communities.Add(communities[0]);

                await _feedOrchestrator.AddAsync(currentUserId, feedItemViewModel);
                var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id, includeProperties: "FeedCommunity");

                Assert.Equal(communities[0].Id, feedFromDb.FeedCommunity.First().CommunityId);
                Assert.True(feedFromDb.FeedCommunity.First().CommunityId > 0);
            }

            [Fact]
            public async Task AddFeedItem_HasCoobook_IdsAreEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                var feedItemViewModel = GetFeedItemViewModel();

                feedItemViewModel.Cookbooks.Add(cookbooks[0]);

                await _feedOrchestrator.AddAsync(currentUserId, feedItemViewModel);
                var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id, includeProperties: "FeedCookbook");

                Assert.Equal(cookbooks[0].Id, feedFromDb.FeedCookbook.First().CookbookId);
                Assert.True(feedFromDb.FeedCookbook.First().CookbookId > 0);
            }

            [Fact]
            public async Task AddFeedItem_HasOrganization_IdsAreEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                var feedItemViewModel = GetFeedItemViewModel();

                feedItemViewModel.Organizations.Add(organizations[0]);
                await _feedOrchestrator.AddAsync(currentUserId, feedItemViewModel);

                var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id, includeProperties: "FeedOrganization");
                Assert.Equal(organizations[0].Id, feedFromDb.FeedOrganization.First().OrganizationId);
                Assert.True(feedFromDb.FeedOrganization.First().OrganizationId > 0);
            }

            [Fact]
            public async Task AddFeedItem_HasProfile_IdsAreEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                var feedItemViewModel = GetFeedItemViewModel();
                feedItemViewModel.Profiles.Add(profiles[0]);

                await _feedOrchestrator.AddAsync(currentUserId, feedItemViewModel);

                var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id, includeProperties: "FeedProfile");
                Assert.Equal(profiles[0].Id, feedFromDb.FeedProfile.First().ProfileId);
                Assert.True(feedFromDb.FeedProfile.First().ProfileId > 0);
            }

            [Fact]
            public async Task AddFeedItem_HasRecipe_IdsAreEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                var feedItemViewModel = GetFeedItemViewModel();
                Recipe recipe = new Recipe()
                {
                    Name = "Ham and Rice Dinner",
                    Description = "Ham and rice for a wonderful dinner.",
                    PrepTime = "10 mins",
                    Cooktime = "15 mins",
                    Servings = "Serves 6"
                };
                _unitOfWork.Recipe.Add(recipe);
                await _unitOfWork.SaveAsync();

                feedItemViewModel.Recipes.Add(recipe);

                await _feedOrchestrator.AddAsync(currentUserId, feedItemViewModel);

                var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id, includeProperties: "FeedRecipe");
                Assert.Equal(recipe.Id, feedFromDb.FeedRecipe.First().RecipeId);
                Assert.True(feedFromDb.FeedRecipe.First().RecipeId > 0);
            }


            [Fact]
            public async Task AddFeedItem_HasTopic_IdsAreEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                var feedItemViewModel = GetFeedItemViewModel();
                Recipe recipe = new Recipe()
                {
                    Name = "Ham and Rice Dinner",
                    Description = "Ham and rice for a wonderful dinner.",
                    PrepTime = "10 mins",
                    Cooktime = "15 mins",
                    Servings = "Serves 6"
                };
                _unitOfWork.Recipe.Add(recipe);
                await _unitOfWork.SaveAsync();

                var topic = _unitOfWork.Topic.AddFromITopicItem(recipe);
                await _unitOfWork.SaveAsync();
                feedItemViewModel.Topics.Add(topic);

                await _feedOrchestrator.AddAsync(currentUserId, feedItemViewModel);

                var feedFromDb = await _unitOfWork.Feed.GetFirstOrDefaultAsync(x => x.Id == feedItemViewModel.Feed.Id, includeProperties: "FeedTopic");
                Assert.True(feedFromDb.FeedTopic.First().TopicId > 0);
                Assert.Equal(topic.Id, feedFromDb.FeedTopic.First().TopicId);
            }


        }
    }
}
