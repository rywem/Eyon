using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.DataAccess.Orchestrators;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.Extensions.Configuration;
using Eyon.DataAccess.DataCalls.IDataCall;
using Eyon.DataAccess.DataCalls;
using Eyon.DataAccess.Security.ISecurity;
using Eyon.Models;
using Eyon.Models.Relationship;
using System.Threading.Tasks;
using Eyon.Models.ViewModels;
using Xunit;
using System.Linq;

namespace Eyon.XTests.UnitTests.DataAccess.Orchestator
{
    public class RecipeOrchestratorTests
    {
        private RecipeOrchestrator _recipeOrchestrator;
        private IUnitOfWork _unitOfWork;
        private Mock<IConfiguration> _mockConfig;
        private IRecipeDataCall _recipeDataCall;
        //private IFeedSecurity _feedSecurity;
        private Mock<IFeedSecurity> _feedSecurity;


        private List<Country> countries;
        private List<State> states;
        private List<Community> communities;
        private List<Cookbook> cookbooks;
        private List<Category> categories;
        private List<ApplicationUser> applicationUsers;

        public RecipeOrchestratorTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(RecipeOrchestratorTests));
            this._mockConfig = new Mock<IConfiguration>();
            this._recipeDataCall = new RecipeDataCall(this._unitOfWork);
            this._feedSecurity = new Mock<IFeedSecurity>();
            this._recipeOrchestrator = new RecipeOrchestrator(this._unitOfWork, this._mockConfig.Object, this._recipeDataCall, this._feedSecurity.Object);
            SeedDatabase();
        }
        
        [Fact]
        public async Task AddAsync_RegularInsert_RecipeShouldInsert()
        {
            string currentUserId = applicationUsers[0].Id;
            RecipeViewModel recipeViewModel = GetRecipeViewModel();
            await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

            var recipe = _unitOfWork.Recipe.Get(recipeViewModel.Recipe.Id);
            Assert.NotNull(recipe);

            var ingredients = _unitOfWork.Ingredient.GetAll(x => x.RecipeId == recipe.Id).ToList();
            Assert.Equal(11, ingredients.Count);

            var instruction = _unitOfWork.Instruction.GetAll(x => x.RecipeId == recipe.Id).ToList();
            Assert.Equal(6, instruction.Count);
        }
        [Fact]
        public async Task AddAsync_RegularInsert_RecipeShouldHaveCorrectOwner()
        {
            string currentUserId = applicationUsers[0].Id;
            RecipeViewModel recipeViewModel = GetRecipeViewModel();
            await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

            var ownedRecipe = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(currentUserId, x => x.Id == recipeViewModel.Recipe.Id);
            Assert.NotNull(ownedRecipe);
        }
        [Fact]
        public async Task AddAsync_ShouldNotBeAccessibleIfNotOwner_ResultShouldBeNull()
        {
            string currentUserId = applicationUsers[0].Id;
            RecipeViewModel recipeViewModel = GetRecipeViewModel();
            await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

            string differentOwner = applicationUsers[1].Id;
            var ownedRecipe = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(differentOwner, x => x.Id == recipeViewModel.Recipe.Id);
            Assert.Null(ownedRecipe);
        }
        [Fact]
        public async Task AddAsync_PrivateAndPublicShouldBeAccessibleIfOwner_CountShouldBe2()
        {
            string currentUserId = applicationUsers[0].Id;
            RecipeViewModel recipeViewModel = GetRecipeViewModel();
            recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Public;
            await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
            
            RecipeViewModel recipeViewModel2 = GetRecipeViewModel();
            recipeViewModel2.Recipe.Privacy = Models.Enums.Privacy.Private;
            await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel2);
            var publicAndPrivateRecipes = await _unitOfWork.Recipe.GetAllAvailableAsync(currentUserId);            
            Assert.Equal(2, publicAndPrivateRecipes.Count());
        }

        [Fact]
        public async Task AddAsync_PublicShouldBeAccessibleIfNotOwner_CountShouldBe1()
        {
            string currentUserId = applicationUsers[0].Id;
            RecipeViewModel recipeViewModel = GetRecipeViewModel();
            recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Public;
            await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

            string notOwnerUserId = applicationUsers[1].Id;
            
            var publicAndPrivateRecipes = await _unitOfWork.Recipe.GetAllAvailableAsync(notOwnerUserId);
            Assert.Single(publicAndPrivateRecipes);
        }

        [Fact]
        public async Task AddAsync_PrivateShouldNotBeAccessibleIfNotOwner_CountShouldBe0()
        {
            string currentUserId = applicationUsers[0].Id;
            RecipeViewModel recipeViewModel = GetRecipeViewModel();
            recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Private;
            await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

            string notOwnerUserId = applicationUsers[1].Id;
            var publicAndPrivateRecipes = await _unitOfWork.Recipe.GetAllAvailableAsync(notOwnerUserId);            
            Assert.Empty(publicAndPrivateRecipes);
        }

        #region Sample Data
        private RecipeViewModel GetRecipeViewModel()
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel.Recipe.Name = "Hobo Stew";
            recipeViewModel.Recipe.Description = "A quick and easy dinner.";
            recipeViewModel.Recipe.PrepTime = "10 mins";
            recipeViewModel.Recipe.Servings = "4";
            recipeViewModel.Recipe.Cooktime = "10 mins";
            recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Public;
            recipeViewModel.CommunityId = communities[0].Id;

            recipeViewModel.IngredientsText = @"1 lb ground beef
1 medium onion
3 cloves garlic
1 can tomato sauce
1 can pinto beans
1 can corn
1 can green beans
1 can peas and carrots
2-3 beef bouillon cubes
1/2 tsp pepper
1 cup cooked rice (optional)";

            recipeViewModel.InstructionsText = @"In a medium pot, saute onions until soft.
Add beef, brown until brown.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm. 
Serve and enjoy!";

            return recipeViewModel;
        }
        private void SeedDatabase()
        {
            Country country = new Country()
            {
                Code = "US",
                Name = "UNITED STATES"
            };
            _unitOfWork.Country.Add(country);
            _unitOfWork.Save();
            countries = new List<Country>();
            countries.Add(country);

            State state = new State()
            {
                Name = "California",
                LocalName = "California",
                Code = "CA",
                Type = "State",
                CountryId = country.Id
            };
            _unitOfWork.State.Add(state);
            _unitOfWork.Save();
            states = new List<State>();
            states.Add(state);

            Community community = new Community() { Name = "QUINCY", Active = true };
            _unitOfWork.Community.Add(community);
            _unitOfWork.Save();
            communities = new List<Community>();
            communities.Add(community);
            _unitOfWork.CommunityState.AddFromEntities(community, state);

            ApplicationUser user1 = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Ryan",
                LastName = "Wemmer"
            };
            ApplicationUser user2 = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Kelly",
                LastName = "Wemmer"
            };
            _unitOfWork.ApplicationUser.Add(user1);
            _unitOfWork.ApplicationUser.Add(user2);
            _unitOfWork.Save();

            applicationUsers = new List<ApplicationUser>();
            applicationUsers.Add(user1);
            applicationUsers.Add(user2);
            // add cookbooks
            // add categories
            Cookbook cookbook1 = new Cookbook()
            {
                Author = "Ryan Wemmer",
                Copyright = "2020",
                Name = "Breakfast Time",
                Description = "Ryan's Breakfast Favorites",
                Privacy = Models.Enums.Privacy.Public
            };
            _unitOfWork.Cookbook.Add(cookbook1);
            cookbooks = new List<Cookbook>();

            Cookbook cookbook2 = new Cookbook()
            {
                Author = "Ryan Wemmer",
                Copyright = "2018",
                Name = "Dinner Explorer",
                Description = "Exploring the greatness that can be dinner.",
                Privacy = Models.Enums.Privacy.Private
            };
            _unitOfWork.Cookbook.Add(cookbook2);
            Cookbook cookbook3 = new Cookbook()
            {
                Author = "Kelly Wemmer",
                Copyright = "2019",
                Name = "Sweets and Treats",
                Description = "My favorite desserts and sweets.",
                Privacy = Models.Enums.Privacy.Public
            };
            _unitOfWork.Cookbook.Add(cookbook3);
            _unitOfWork.Save();
            cookbooks.Add(cookbook1);
            cookbooks.Add(cookbook2);
            cookbooks.Add(cookbook3);

            _unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook1);
            _unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook2);
            _unitOfWork.ApplicationUserCookbook.AddFromEntities(user2, cookbook3);
            _unitOfWork.Save();

            Category category1 = new Category()
            {
                Name = "Dinner",
                DisplayOrder = 0
            };
            _unitOfWork.Category.Add(category1);

            Category category2 = new Category()
            {
                Name = "Beef",
                DisplayOrder = 1
            };
            _unitOfWork.Category.Add(category2);
            _unitOfWork.Save();
            categories = new List<Category>();
            categories.Add(category1);
            categories.Add(category2);
        }
        #endregion
    }
}
