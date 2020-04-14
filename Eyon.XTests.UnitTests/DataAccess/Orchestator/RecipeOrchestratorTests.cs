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

        public RecipeOrchestratorTests()
        {

        }
        
        public class AddAsyncTests
        {
            private RecipeOrchestrator _recipeOrchestrator;
            private IUnitOfWork _unitOfWork;
            private Mock<IConfiguration> _mockConfig;
            private IRecipeDataCall _recipeDataCall;
            private Mock<IFeedSecurity> _feedSecurity;
            private List<Country> countries;
            private List<State> states;
            private List<Community> communities;
            private List<Cookbook> cookbooks;
            private List<Category> categories;
            private List<ApplicationUser> applicationUsers;
            public AddAsyncTests()
            {
                this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(AddAsyncTests));
                this._mockConfig = new Mock<IConfiguration>();
                this._recipeDataCall = new RecipeDataCall(this._unitOfWork);
                this._feedSecurity = new Mock<IFeedSecurity>();
                this._recipeOrchestrator = new RecipeOrchestrator(this._unitOfWork, this._mockConfig.Object, this._recipeDataCall, this._feedSecurity.Object);
                countries = new List<Country>();
                states =new List<State>();
                communities = new List<Community>();
                cookbooks = new List<Cookbook>();
                categories = new List<Category>();
                applicationUsers = new List<ApplicationUser>();
                SeedDatabase(this._unitOfWork, this.countries, this.states, this.communities, this.applicationUsers, this.cookbooks, this.categories);
            }

            [Fact]
            public async Task InsertRecipe_RecipeShouldNotBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipe = _unitOfWork.Recipe.Get(recipeViewModel.Recipe.Id);
                Assert.NotNull(recipe);
            }

            [Fact]
            public async Task AddCommunity_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CommunityRecipe,CommunityRecipe.Community");

                Assert.NotNull(recipe.CommunityRecipe);
                Assert.NotNull(recipe.CommunityRecipe.Community);
            }

            #region Category
            [Fact]
            public async Task AddCategory_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "RecipeCategory");
                Assert.Single(recipe.RecipeCategory);
            }
            [Fact]
            public async Task Add2Categories_CountShouldBe2()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString();
                recipeViewModel.CategorySelector.ItemIds += "," + categories[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "RecipeCategory");
                Assert.Equal(2, recipe.RecipeCategory.Count);
            }

            #endregion

            #region Cookbook
            [Fact]
            public async Task AddCookbook_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CookbookRecipe");
                Assert.Single(recipe.CookbookRecipe);
            }

            [Fact]
            public async Task Add2Cookbooks_CountShouldBe2()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CookbookRecipe");
                Assert.Equal(2, recipe.CookbookRecipe.Count);
            }

            [Fact]
            public async Task AddCookbookButNotCookbookOwner_ThrowSafeExceptionDenied()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[2].Id.ToString();

                Action action = async () => await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var ex = await Assert.ThrowsAsync<Models.Errors.SafeException>(async () => await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel));
                Assert.Equal((int)ex.ErrorType, (int)Models.Enums.ErrorType.Denied);
            }
            #endregion

            #region Ingredients and Instructions
            [Fact]
            public async Task InsertInstructions_CountShouldBe6()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var instruction = _unitOfWork.Instruction.GetAll(x => x.RecipeId == recipeViewModel.Recipe.Id).ToList();
                Assert.Equal(6, instruction.Count);
            }

            [Fact]
            public async Task InsertInstructions_ShouldBeInsertedInOrder()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var instruction = _unitOfWork.Instruction.GetAll(x => x.RecipeId == recipeViewModel.Recipe.Id).OrderBy(x => x.StepNumber).ToList();

                Assert.Equal("In a medium pot, saute onions until soft.", instruction[0].Text);
                Assert.Equal(1, instruction[0].StepNumber);

                Assert.Equal("Add beef, brown until brown.", instruction[1].Text);
                Assert.Equal(2, instruction[1].StepNumber);

                Assert.Equal("Add garlic, until fragrant.", instruction[2].Text);
                Assert.Equal(3, instruction[2].StepNumber);

                Assert.Equal("Drain excess juices.", instruction[3].Text);
                Assert.Equal(4, instruction[3].StepNumber);

                Assert.Equal("Add all remaining ingredients, heat until warm.", instruction[4].Text);
                Assert.Equal(5, instruction[4].StepNumber);

                Assert.Equal("Serve and enjoy!", instruction[5].Text);
                Assert.Equal(6, instruction[5].StepNumber);
            }

            [Fact]
            public async Task InsertIngredients_ShouldBeInsertedInOrder()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var ingredients = _unitOfWork.Ingredient.GetAll(x => x.RecipeId == recipeViewModel.Recipe.Id).OrderBy(x => x.Number).ToList();

                Assert.Equal("1 lb ground beef", ingredients[0].Text);
                Assert.Equal(1, ingredients[0].Number);

                Assert.Equal("1 medium onion", ingredients[1].Text);
                Assert.Equal(2, ingredients[1].Number);

                Assert.Equal("3 cloves garlic", ingredients[2].Text);
                Assert.Equal(3, ingredients[2].Number);

                Assert.Equal("1 can tomato sauce", ingredients[3].Text);
                Assert.Equal(4, ingredients[3].Number);

                Assert.Equal("1 can pinto beans", ingredients[4].Text);
                Assert.Equal(5, ingredients[4].Number);

                Assert.Equal("1 can corn", ingredients[5].Text);
                Assert.Equal(6, ingredients[5].Number);

                Assert.Equal("1 can green beans", ingredients[6].Text);
                Assert.Equal(7, ingredients[6].Number);

                Assert.Equal("1 can peas and carrots", ingredients[7].Text);
                Assert.Equal(8, ingredients[7].Number);

                Assert.Equal("2-3 beef bouillon cubes", ingredients[8].Text);
                Assert.Equal(9, ingredients[8].Number);

                Assert.Equal("1/2 tsp pepper", ingredients[9].Text);
                Assert.Equal(10, ingredients[9].Number);

                Assert.Equal("1 cup cooked rice (optional)", ingredients[10].Text);
                Assert.Equal(11, ingredients[10].Number);
            }

            [Fact]
            public async Task InsertIngredients_CountShouldBe11()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var ingredients = _unitOfWork.Ingredient.GetAll(x => x.RecipeId == recipeViewModel.Recipe.Id).ToList();
                Assert.Equal(11, ingredients.Count);
            }

            #endregion

            #region Ownership and Privacy
            [Fact]
            public async Task RegularInsert_RecipeShouldHaveCorrectOwner()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var ownedRecipe = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(currentUserId, x => x.Id == recipeViewModel.Recipe.Id);
                Assert.NotNull(ownedRecipe);
            }
            [Fact]
            public async Task ShouldNotBeAccessibleIfNotOwner_ResultShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                string differentOwner = applicationUsers[1].Id;
                var ownedRecipe = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(differentOwner, x => x.Id == recipeViewModel.Recipe.Id);
                Assert.Null(ownedRecipe);
            }
            [Fact]
            public async Task PrivateShouldBeAccessibleIfOwner_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Private;
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeEnumerable = await _unitOfWork.Recipe.GetAllAvailableAsync(currentUserId);
                Assert.Single(recipeEnumerable);
            }

            [Fact]
            public async Task PublicShouldBeAccessibleIfOwner_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Public;
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeEnumerable = await _unitOfWork.Recipe.GetAllAvailableAsync(currentUserId);
                Assert.Single(recipeEnumerable);
            }

            [Fact]
            public async Task PublicShouldBeAccessibleIfNotOwner_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Public;
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                string notOwnerUserId = applicationUsers[1].Id;

                var recipeEnumerable = await _unitOfWork.Recipe.GetAllAvailableAsync(notOwnerUserId);
                Assert.Single(recipeEnumerable);
            }

            [Fact]
            public async Task PrivateShouldNotBeAccessibleIfNotOwner_CountShouldBe0()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Private;
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                string notOwnerUserId = applicationUsers[1].Id;
                var recipeEnumerable = await _unitOfWork.Recipe.GetAllAvailableAsync(notOwnerUserId);
                Assert.Empty(recipeEnumerable);
            }
            #endregion

        }
 
        public class GetAsyncTests
        {
            private RecipeOrchestrator _recipeOrchestrator;
            private IUnitOfWork _unitOfWork;
            private Mock<IConfiguration> _mockConfig;
            private IRecipeDataCall _recipeDataCall;
            private Mock<IFeedSecurity> _feedSecurity;
            private List<Country> countries;
            private List<State> states;
            private List<Community> communities;
            private List<Cookbook> cookbooks;
            private List<Category> categories;
            private List<ApplicationUser> applicationUsers;
            public GetAsyncTests()
            {
                this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(GetAsyncTests));
                this._mockConfig = new Mock<IConfiguration>();
                this._recipeDataCall = new RecipeDataCall(this._unitOfWork);
                this._feedSecurity = new Mock<IFeedSecurity>();
                this._recipeOrchestrator = new RecipeOrchestrator(this._unitOfWork, this._mockConfig.Object, this._recipeDataCall, this._feedSecurity.Object);
                countries = new List<Country>();
                states = new List<State>();
                communities = new List<Community>();
                cookbooks = new List<Cookbook>();
                categories = new List<Category>();
                applicationUsers = new List<ApplicationUser>();
                SeedDatabase(this._unitOfWork, this.countries, this.states, this.communities, this.applicationUsers, this.cookbooks, this.categories);
            }
            [Fact]
            public async Task Recipe_ShouldNotBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.NotNull(recipeViewModelFromDb);
                Assert.Equal(recipeViewModel.Recipe.Id, recipeViewModelFromDb.Recipe.Id);
                Assert.Equal(recipeViewModel.Recipe.Name, recipeViewModelFromDb.Recipe.Name);
            }

            [Fact]
            public async Task Community_ShouldNotBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.NotNull(recipeViewModelFromDb.Community);
                Assert.Equal(communities[0].Id, recipeViewModelFromDb.Community.Id);
            }

            [Fact]
            public async Task CommunityName_ShouldEqualExpected()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                string expected = "Quincy, CA (United States)";
                Assert.Equal(expected, recipeViewModelFromDb.CommunityName);
            }

            [Fact]
            public async Task CommunityId_ShouldEqualInputId()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(recipeViewModelFromDb.CommunityId, recipeViewModelFromDb.Community.Id);
            }

            [Fact]
            public async Task IsNotOwner_IsOwnerShouldBeFalse()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                string otherUserId = applicationUsers[1].Id;
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(otherUserId, recipeViewModel.Recipe.Id);

                Assert.False(recipeViewModelFromDb.IsOwner);                
            }

            [Fact]
            public async Task IsOwner_IsOwnerShouldBeTrue()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.True(recipeViewModelFromDb.IsOwner);
            }

            [Fact]
            public async Task Instructions_CountShouldEqual6()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);                
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Equal(6, recipeViewModelFromDb.Instruction.Count);
            }

            [Fact]
            public async Task InstructionsTxt_ShouldEqualExpectedString()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);                
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                
                string expected = @"In a medium pot, saute onions until soft.
Add beef, brown until brown.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm.
Serve and enjoy!";
                Assert.Equal(expected, recipeViewModelFromDb.InstructionsText);
            }
        }

        #region Sample Data
        public static RecipeViewModel GetRecipeViewModel(Community community)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel.Recipe.Name = "Hobo Stew";
            recipeViewModel.Recipe.Description = "A quick and easy dinner.";
            recipeViewModel.Recipe.PrepTime = "10 mins";
            recipeViewModel.Recipe.Servings = "4";
            recipeViewModel.Recipe.Cooktime = "10 mins";
            recipeViewModel.Recipe.Privacy = Models.Enums.Privacy.Public;
            recipeViewModel.CommunityId = community.Id;

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
        public static void SeedDatabase(IUnitOfWork unitOfWork, List<Country> countries, List<State> states, List<Community> communities,
            List<ApplicationUser> applicationUsers, List<Cookbook> cookbooks, List<Category> categories)
        {
            Country country = new Country()
            {
                Code = "US",
                Name = "UNITED STATES"
            };
            unitOfWork.Country.Add(country);
            unitOfWork.Save();            
            countries.Add(country);

            State state = new State()
            {
                Name = "California",
                LocalName = "California",
                Code = "CA",
                Type = "State",
                CountryId = country.Id
            };
            unitOfWork.State.Add(state);
            unitOfWork.Save();            
            states.Add(state);

            Community community = new Community() { Name = "QUINCY", Active = true, CountryId = country.Id };
            unitOfWork.Community.Add(community);
            unitOfWork.Save();            
            communities.Add(community);
            unitOfWork.CommunityState.AddFromEntities(community, state);

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
            unitOfWork.ApplicationUser.Add(user1);
            unitOfWork.ApplicationUser.Add(user2);
            unitOfWork.Save();
            
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
            unitOfWork.Cookbook.Add(cookbook1);
            

            Cookbook cookbook2 = new Cookbook()
            {
                Author = "Ryan Wemmer",
                Copyright = "2018",
                Name = "Dinner Explorer",
                Description = "Exploring the greatness that can be dinner.",
                Privacy = Models.Enums.Privacy.Private
            };
            unitOfWork.Cookbook.Add(cookbook2);
            Cookbook cookbook3 = new Cookbook()
            {
                Author = "Kelly Wemmer",
                Copyright = "2019",
                Name = "Sweets and Treats",
                Description = "My favorite desserts and sweets.",
                Privacy = Models.Enums.Privacy.Public
            };
            unitOfWork.Cookbook.Add(cookbook3);
            unitOfWork.Save();
            cookbooks.Add(cookbook1);
            cookbooks.Add(cookbook2);
            cookbooks.Add(cookbook3);

            unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook1);
            unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook2);
            unitOfWork.ApplicationUserCookbook.AddFromEntities(user2, cookbook3);
            unitOfWork.Save();

            Category category1 = new Category()
            {
                Name = "Dinner",
                DisplayOrder = 0
            };
            unitOfWork.Category.Add(category1);

            Category category2 = new Category()
            {
                Name = "Beef",
                DisplayOrder = 1
            };
            unitOfWork.Category.Add(category2);
            unitOfWork.Save();            
            categories.Add(category1);
            categories.Add(category2);
        }
        #endregion
    }
}
