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
using Eyon.DataAccess.Orchestrators.IOrchestrator;
using Eyon.DataAccess.Images;

namespace Eyon.XTests.UnitTests.DataAccess.Orchestator
{
    public class RecipeOrchestratorTests
    {
        public class AddAsyncTests
        {
            private RecipeOrchestrator _recipeOrchestrator;
            private IUnitOfWork _unitOfWork;
            private IRecipeDataCall _recipeDataCall;
            private Mock<IFeedSecurity> _feedSecurity;
            private Mock<IImageHelper> _imageHelper;
            private List<Country> countries;
            private List<State> states;
            private List<Community> communities;
            private List<Cookbook> cookbooks;
            private List<Category> categories;
            private List<ApplicationUser> applicationUsers;
            public AddAsyncTests()
            {
                this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(AddAsyncTests));
                this._recipeDataCall = new RecipeDataCall(this._unitOfWork);
                this._feedSecurity = new Mock<IFeedSecurity>();
                this._imageHelper = new Mock<IImageHelper>();
                this._recipeOrchestrator = new RecipeOrchestrator(this._unitOfWork, this._recipeDataCall, this._feedSecurity.Object, this._imageHelper.Object);
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

            #region UserImage
            
            [Fact]
            public async Task AddUserImage_ShouldNotBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                string guid = Guid.NewGuid().ToString();
                UserImage userImage = new UserImage()
                {
                    FileName = guid + ".jpg",
                    FileNameThumb = guid + "_thumb.jpg",
                    FileType = "jpg",
                    Privacy = Models.Enums.Privacy.Public
                };
                recipeViewModel.UserImage = new List<UserImage>();
                recipeViewModel.UserImage.Add(userImage);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "RecipeUserImage,RecipeUserImage.UserImage");

                Assert.NotNull(recipe.RecipeUserImage.FirstOrDefault().UserImage);
            }
            #endregion 

            #region Community, State, Country
            [Fact]
            public async Task AddCommunity_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CommunityRecipe,CommunityRecipe.Community");

                Assert.NotNull(recipe.CommunityRecipe);
                Assert.NotNull(recipe.CommunityRecipe.Community);
                Assert.Equal(communities[0].Id,recipe.CommunityRecipe.Community.Id);
            }

            [Fact]
            public async Task AddCommunity_StateShouldBeSet_IdsShouldBeEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CommunityRecipe,CommunityRecipe.Community,CommunityRecipe.Community.CommunityState,CommunityRecipe.Community.CommunityState.State");
                
                Assert.NotNull(recipe.CommunityRecipe.Community.CommunityState);
                Assert.NotNull(recipe.CommunityRecipe.Community.CommunityState.State);
                Assert.Equal(states[0].Id, recipe.CommunityRecipe.Community.CommunityState.State.Id);
            }
            [Fact]
            public async Task AddCommunity_CountryShouldBeSet_IdsShouldBeEqual()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CommunityRecipe,CommunityRecipe.Community,CommunityRecipe.Community.Country");

                Assert.NotNull(recipe.CommunityRecipe.Community.Country);                
                Assert.Equal(countries[0].Id, recipe.CommunityRecipe.Community.Country.Id);
            }

            #endregion 

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

                var instruction = _unitOfWork.Instruction.GetAll(x => x.RecipeId == recipeViewModel.Recipe.Id).OrderBy(x => x.Count).ToList();

                Assert.Equal("In a medium pot, saute onions until soft.", instruction[0].Text);
                Assert.Equal(1, instruction[0].Count);

                Assert.Equal("Add beef, cook until brown.", instruction[1].Text);
                Assert.Equal(2, instruction[1].Count);

                Assert.Equal("Add garlic, until fragrant.", instruction[2].Text);
                Assert.Equal(3, instruction[2].Count);

                Assert.Equal("Drain excess juices.", instruction[3].Text);
                Assert.Equal(4, instruction[3].Count);

                Assert.Equal("Add all remaining ingredients, heat until warm.", instruction[4].Text);
                Assert.Equal(5, instruction[4].Count);

                Assert.Equal("Serve and enjoy!", instruction[5].Text);
                Assert.Equal(6, instruction[5].Count);
            }

            [Fact]
            public async Task InsertIngredients_ShouldBeInsertedInOrder()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var ingredients = _unitOfWork.Ingredient.GetAll(x => x.RecipeId == recipeViewModel.Recipe.Id).OrderBy(x => x.Count).ToList();

                Assert.Equal("1 lb ground beef", ingredients[0].Text);
                Assert.Equal(1, ingredients[0].Count);

                Assert.Equal("1 medium onion", ingredients[1].Text);
                Assert.Equal(2, ingredients[1].Count);

                Assert.Equal("3 cloves garlic", ingredients[2].Text);
                Assert.Equal(3, ingredients[2].Count);

                Assert.Equal("1 can tomato sauce", ingredients[3].Text);
                Assert.Equal(4, ingredients[3].Count);

                Assert.Equal("1 can pinto beans", ingredients[4].Text);
                Assert.Equal(5, ingredients[4].Count);

                Assert.Equal("1 can corn", ingredients[5].Text);
                Assert.Equal(6, ingredients[5].Count);

                Assert.Equal("1 can green beans", ingredients[6].Text);
                Assert.Equal(7, ingredients[6].Count);

                Assert.Equal("1 can peas and carrots", ingredients[7].Text);
                Assert.Equal(8, ingredients[7].Count);

                Assert.Equal("2-3 beef bouillon cubes", ingredients[8].Text);
                Assert.Equal(9, ingredients[8].Count);

                Assert.Equal("1/2 tsp pepper", ingredients[9].Text);
                Assert.Equal(10, ingredients[9].Count);

                Assert.Equal("1 cup cooked rice (optional)", ingredients[10].Text);
                Assert.Equal(11, ingredients[10].Count);
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
            private Mock<IImageHelper> _imageHelper;
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
                this._recipeDataCall = new RecipeDataCall(this._unitOfWork);
                this._feedSecurity = new Mock<IFeedSecurity>();
                this._imageHelper = new Mock<IImageHelper>();
                this._recipeOrchestrator = new RecipeOrchestrator(this._unitOfWork,  this._recipeDataCall, this._feedSecurity.Object, this._imageHelper.Object);
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

            #region UserImage

            [Fact]
            public async Task GetUserImages_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                string guid = Guid.NewGuid().ToString();
                UserImage userImage = new UserImage()
                {
                    FileName = guid + ".jpg",
                    FileNameThumb = guid + "_thumb.jpg",
                    FileType = "jpg",
                    Privacy = Models.Enums.Privacy.Public
                };
                recipeViewModel.UserImage = new List<UserImage>();
                recipeViewModel.UserImage.Add(userImage);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Single(recipeViewModelFromDb.UserImage);
            }
            #endregion

            #region Cookbook

            [Fact]
            public async Task CookbookSelector_CountShouldBe2()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(2, recipeViewModelFromDb.CookbookSelector.Items.Count);
            }

            #endregion

            #region Category

            [Fact]
            public async Task CategorySelector_CountShouldBe2()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString();
                recipeViewModel.CategorySelector.ItemIds += "," + categories[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(2, recipeViewModelFromDb.CategorySelector.Items.Count);
            }

            [Fact]
            public async Task UserImage_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                string guid = Guid.NewGuid().ToString();
                UserImage userImage = new UserImage()
                {
                    FileName = guid + ".jpg",
                    FileNameThumb = guid + "_thumb.jpg",
                    FileType = "jpg",
                    Privacy = Models.Enums.Privacy.Public
                };
                recipeViewModel.UserImage = new List<UserImage>();
                recipeViewModel.UserImage.Add(userImage);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Single(recipeViewModelFromDb.UserImage);
            }

            #endregion 

            #region Community
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
            #endregion

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

            #region Instructions and Ingredients
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
            public async Task InstructionsText_ShouldEqualExpectedString()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                string expected = @"In a medium pot, saute onions until soft.
Add beef, cook until brown.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm.
Serve and enjoy!";
                Assert.Equal(expected, recipeViewModelFromDb.InstructionText);
            }

            
            [Fact]
            public async Task Ingredients_CountShouldEqual11()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Equal(11, recipeViewModelFromDb.Ingredient.Count);
            }

            [Fact]
            public async Task IngredientsText_ShouldEqualExpectedString()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                string expected = @"1 lb ground beef
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
                Assert.Equal(expected, recipeViewModelFromDb.IngredientText);
            }
            #endregion
        }

        public class UpdateAsyncTests
        {
            private RecipeOrchestrator _recipeOrchestrator;
            private IUnitOfWork _unitOfWork;
            private IRecipeDataCall _recipeDataCall;
            private Mock<IImageHelper> _imageHelper;
            private Mock<IFeedSecurity> _feedSecurity;
            private List<Country> countries;
            private List<State> states;
            private List<Community> communities;
            private List<Cookbook> cookbooks;
            private List<Category> categories;
            private List<ApplicationUser> applicationUsers;
            private List<Recipe> recipes { get; set; }
            public UpdateAsyncTests()
            {
                this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(UpdateAsyncTests));
                this._imageHelper = new Mock<IImageHelper>();
                this._recipeDataCall = new RecipeDataCall(this._unitOfWork);
                this._feedSecurity = new Mock<IFeedSecurity>();                
                this._recipeOrchestrator = new RecipeOrchestrator(this._unitOfWork, this._recipeDataCall, this._feedSecurity.Object, this._imageHelper.Object);
                countries = new List<Country>();
                states = new List<State>();
                communities = new List<Community>();
                cookbooks = new List<Cookbook>();
                categories = new List<Category>();
                applicationUsers = new List<ApplicationUser>();
                SeedDatabase(this._unitOfWork, this.countries, this.states, this.communities, this.applicationUsers, this.cookbooks, this.categories);
            }
            
            #region Instructions
            [Fact]
            public async Task RemoveInstruction_InstructionsShouldBe5()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelJustAddedToDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelJustAddedToDb.InstructionText = @"Add beef, cook until brown.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm.
Serve and enjoy!";
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelJustAddedToDb);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(5, recipeViewModelFromDb.Instruction.Count);
            }
            [Fact]
            public async Task AddInstruction_ShouldBe7()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelJustAddedToDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelJustAddedToDb.InstructionText = @"In a medium pot, saute onions until soft.
Add beef, cook until brown.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm.
Do not boil
Serve and enjoy!";
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelJustAddedToDb);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(7, recipeViewModelFromDb.Instruction.Count);
            }

            [Fact]
            public async Task RemoveInstructionText_ShouldEqualExpected()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelChanged = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelChanged.InstructionText = @"Add beef, cook until brown.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm.
Serve and enjoy!";
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelChanged);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(recipeViewModelChanged.InstructionText, recipeViewModelFromDb.InstructionText);
            }

            [Fact]
            public async Task RearrageInstructionText_ShouldEqualExpected()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelChanged = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelChanged.InstructionText = @"Add beef, cook until brown.
In a medium pot, saute onions until soft.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm.
Serve and enjoy!";
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelChanged);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(recipeViewModelChanged.InstructionText, recipeViewModelFromDb.InstructionText);
            }

            [Fact]
            public async Task ReplaceInstructionText_ShouldEqualExpected()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelChanged = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelChanged.InstructionText = @"In a medium pot, saute onions until soft.
Add ground beef, browning.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm.
Serve and enjoy!";
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelChanged);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(recipeViewModelChanged.InstructionText, recipeViewModelFromDb.InstructionText);
            }
            #endregion
            
            #region Ingredients
            [Fact]
            public async Task RemoveIngredient_CountShouldBe10()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.IngredientText = @"1 lb ground beef
3 cloves garlic
1 can tomato sauce
1 can pinto beans
1 can corn
1 can green beans
1 can peas and carrots
2-3 beef bouillon cubes
1/2 tsp pepper
1 cup cooked rice (optional)";
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(10, recipeViewModelFromDb.Ingredient.Count);
            }

            [Fact]
            public async Task AddIngredient_CountShouldBe12()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.IngredientText = @"1 lb ground beef
1 medium onion
3 cloves garlic
1 can tomato sauce
1 can pinto beans
1 can corn
1 can green beans
1 can peas and carrots
2-3 beef bouillon cubes
1/2 tsp pepper
1 cup cooked rice (optional)
1 shallot";
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(12, recipeViewModelFromDb.Ingredient.Count);
            }

            [Fact]
            public async Task RearrageIngredient_ShouldEqualExpected()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);                
                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.IngredientText = @"1 lb ground beef
1 can corn
1 can green beans
1 can peas and carrots
2-3 beef bouillon cubes
1 medium onion
3 cloves garlic
1 can tomato sauce
1 can pinto beans
1/2 tsp pepper
1 cup cooked rice (optional)"; 
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(recipeViewModelForUpdating.IngredientText, recipeViewModelFromDb.IngredientText);
            }
            [Fact]
            public async Task ReplaceIngredient_ShouldEqualExpected()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.IngredientText = @"1 lb ground beef
1 can corn
1 can green beans
1 can peas and carrots
2-3 beef bouillon cubes
1 shallot
3 cloves garlic
1 can tomato sauce
1 can pinto beans
1/2 tsp pepper
1 cup cooked rice (optional)";
                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                Assert.Equal(recipeViewModelForUpdating.IngredientText, recipeViewModelFromDb.IngredientText);
            }
            #endregion
            
            #region Cookbook
            [Fact]
            public async Task AddAnotherCookbook_CountShouldBe3()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CookbookSelector.ItemIds  += "," + cookbooks[3].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Equal(3, recipeViewModelFromDb.CookbookSelector.Items.Count);
            }

            [Fact]
            public async Task AddAnotherCookbook_2Cookbooks_2IdsShouldBe2Inputs()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();                
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Equal(cookbooks[0].Id, recipeViewModelFromDb.CookbookSelector.Items[0].Id);
                Assert.Equal(cookbooks[1].Id, recipeViewModelFromDb.CookbookSelector.Items[1].Id);
                
            }

            [Fact]
            public async Task Remove2Cookbooks_CountShouldBe1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString() + "," + cookbooks[3].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Single(recipeViewModelFromDb.CookbookSelector.Items);
            }
            [Fact]
            public async Task Remove2Cookbooks_OneCookbookRemaining_IdShouldEqualInput()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString() + "," + cookbooks[3].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Equal(cookbooks[0].Id, recipeViewModelFromDb.CookbookSelector.Items[0].Id);
            }


            [Fact]
            public async Task ReplaceCookbook_CountShouldBe2()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CookbookSelector.ItemIds = cookbooks[0].Id.ToString() + "," + cookbooks[3].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Equal(2, recipeViewModelFromDb.CookbookSelector.Items.Count);
            }

            [Fact]
            public async Task ReplaceCookbook_TwoCookbooks_2IdsShouldEqual2Inputs()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CookbookSelector.ItemIds = cookbooks[0].Id.ToString() + "," + cookbooks[3].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);

                Assert.Equal(cookbooks[0].Id, recipeViewModelFromDb.CookbookSelector.Items[0].Id);
                Assert.Equal(cookbooks[3].Id, recipeViewModelFromDb.CookbookSelector.Items[1].Id);
            }

            [Fact]
            public async Task ReplaceCookbook_NotOwnerOfOneReplacement_ShouldThrowException()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CookbookSelector.ItemIds = cookbooks[0].Id.ToString() + "," + cookbooks[2].Id.ToString();

                var ex = await Assert.ThrowsAsync<Models.Errors.SafeException>(async () => await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating));
                Assert.Equal((int)ex.ErrorType, (int)Models.Enums.ErrorType.Denied);
            }
            #endregion

            #region Category
            [Fact]
            public async Task AddAnotherCategory_CountEqual3()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString() + "," + categories[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CategorySelector.ItemIds += "," + categories[2].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModelForUpdating.Recipe.Id);

                Assert.Equal(3, recipeViewModelFromDb.CategorySelector.Items.Count);
            }

            [Fact]
            public async Task AddAnotherCategory_Has2Categories_2IdsShouldEqual2Inputs()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CategorySelector.ItemIds += "," + categories[1].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModelForUpdating.Recipe.Id);

                Assert.Equal(categories[0].Id, recipeViewModelFromDb.CategorySelector.Items[0].Id);
                Assert.Equal(categories[1].Id, recipeViewModelFromDb.CategorySelector.Items[1].Id);
            }

            [Fact]
            public async Task RemoveACategory_CountEqual1()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString() + 
                    "," + categories[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CategorySelector.ItemIds = categories[0].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModelForUpdating.Recipe.Id);

                Assert.Single(recipeViewModelFromDb.CategorySelector.Items);
            }

            [Fact]
            public async Task RemoveACategory_IdShouldEqualInput()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString() +
                    "," + categories[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CategorySelector.ItemIds = categories[0].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModelForUpdating.Recipe.Id);

                Assert.Equal(categories[0].Id, recipeViewModelFromDb.CategorySelector.Items[0].Id);
            }

            [Fact]
            public async Task ReplaceACategory_2Categories_CountShouldBe2()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString() +
                    "," + categories[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CategorySelector.ItemIds = categories[1].Id.ToString() + "," + categories[2].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModelForUpdating.Recipe.Id);

                Assert.Equal(2, recipeViewModelFromDb.CategorySelector.Items.Count);
            }

            [Fact]
            public async Task ReplaceACategory_2Categories_2IdsShouldEqual2Inputs()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString() +
                    "," + categories[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CategorySelector.ItemIds = categories[1].Id.ToString() + "," + categories[2].Id.ToString();

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModelForUpdating.Recipe.Id);

                Assert.Equal(categories[1].Id, recipeViewModelFromDb.CategorySelector.Items[0].Id);
                Assert.Equal(categories[2].Id, recipeViewModelFromDb.CategorySelector.Items[1].Id);
            }
            #endregion

            #region Community
            [Fact]
            public async Task ChangeCommunity_ShouldEqualInput()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeViewModelForUpdating = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModel.Recipe.Id);
                recipeViewModelForUpdating.CommunityId = communities[1].Id;

                await _recipeOrchestrator.UpdateAsync(currentUserId, recipeViewModelForUpdating);
                var recipeViewModelFromDb = await _recipeOrchestrator.GetAsync(currentUserId, recipeViewModelForUpdating.Recipe.Id);

                Assert.Equal(communities[1].Id, recipeViewModelFromDb.CommunityId);
                Assert.Equal(communities[1].Id, recipeViewModelFromDb.Community.Id);
            }
            #endregion
        }

        public class DeleteAsyncTests
        {
            private RecipeOrchestrator _recipeOrchestrator;
            private IUnitOfWork _unitOfWork;
            private IRecipeDataCall _recipeDataCall;
            private Mock<IFeedSecurity> _feedSecurity;
            private Mock<IImageHelper> _imageHelper;
            private List<Country> countries;
            private List<State> states;
            private List<Community> communities;
            private List<Cookbook> cookbooks;
            private List<Category> categories;
            private List<ApplicationUser> applicationUsers;
            private List<Recipe> recipes { get; set; }
            public DeleteAsyncTests()
            {
                this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(DeleteAsyncTests));
                this._recipeDataCall = new RecipeDataCall(this._unitOfWork);
                this._feedSecurity = new Mock<IFeedSecurity>();
                this._imageHelper = new Mock<IImageHelper>();
                _imageHelper.Setup(x => x.TryDeleteAsync(It.IsAny<string>())).Returns(Task.Run(() => true));
                this._recipeOrchestrator = new RecipeOrchestrator(this._unitOfWork, this._recipeDataCall, this._feedSecurity.Object, _imageHelper.Object);
                countries = new List<Country>();
                states = new List<State>();
                communities = new List<Community>();
                cookbooks = new List<Cookbook>();
                categories = new List<Category>();
                applicationUsers = new List<ApplicationUser>();
                SeedDatabase(this._unitOfWork, this.countries, this.states, this.communities, this.applicationUsers, this.cookbooks, this.categories);
            }
            [Fact]
            public async Task DeleteRecipe_RecipeShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipe = _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id);
                Assert.True(recipe.Id > 0);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var recipeDeleted = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id);
                Assert.Null(recipeDeleted);
            }
            [Fact]
            public async Task DeleteRecipe_HasCommunity_ShouldBeRemoved()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CommunityRecipe,CommunityRecipe.Community");
                var communityId = recipe.CommunityRecipe.CommunityId;
                var recipeId = recipe.CommunityRecipe.RecipeId;
                Assert.NotNull(recipe.CommunityRecipe);
                Assert.NotNull(recipe.CommunityRecipe.Community);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var recipeDeleted = await _unitOfWork.CommunityRecipe.GetFirstOrDefaultAsync(x => x.CommunityId == communityId && x.RecipeId == recipeId);
                Assert.Null(recipeDeleted);
            }

            [Fact]
            public async Task DeleteRecipe_HadInstruction_ShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);

                var recipeId = recipeViewModel.Recipe.Id;
                var instructionNotDeleted = await _unitOfWork.Instruction.GetFirstOrDefaultAsync(x => x.RecipeId == recipeId);
                Assert.NotNull(instructionNotDeleted);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var instructionDeleted = await _unitOfWork.Instruction.GetFirstOrDefaultAsync(x => x.RecipeId == recipeId);
                Assert.Null(instructionDeleted);
            }

            [Fact]
            public async Task DeleteRecipe_HadIngredient_ShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);                
                var recipeId = recipeViewModel.Recipe.Id;
                var ingredientNotDeleted= await _unitOfWork.Ingredient.GetFirstOrDefaultAsync(x => x.RecipeId == recipeId);
                Assert.NotNull(ingredientNotDeleted);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var ingredientDeleted = await _unitOfWork.Ingredient.GetFirstOrDefaultAsync(x => x.RecipeId == recipeId);
                Assert.Null(ingredientDeleted);
            }

            [Fact]
            public async Task DeleteRecipe_HadImage_ShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                string guid = Guid.NewGuid().ToString();
                UserImage userImage = new UserImage()
                {
                    FileName = guid + ".jpg",
                    FileNameThumb = guid + "_thumb.jpg",
                    FileType = "jpg",
                    Privacy = Models.Enums.Privacy.Public
                };
                recipeViewModel.UserImage = new List<UserImage>();
                recipeViewModel.UserImage.Add(userImage);
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var recipeId = recipeViewModel.Recipe.Id;
                var imageNotDeleted = await _unitOfWork.UserImage.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.UserImage.First().Id);
                Assert.NotNull(imageNotDeleted);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var imageDeleted = await _unitOfWork.UserImage.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.UserImage.First().Id);
                Assert.Null(imageDeleted);
            }

            [Fact]
            public async Task DeleteRecipe_Had2Categories_ShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString();
                recipeViewModel.CategorySelector.ItemIds += "," + categories[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var categoryNotDeleted = await _unitOfWork.RecipeCategory.GetFirstOrDefaultAsync(x => x.RecipeId == recipeViewModel.Recipe.Id);
                Assert.NotNull(categoryNotDeleted);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var categoryDeleted = await _unitOfWork.RecipeCategory.GetFirstOrDefaultAsync(x => x.RecipeId == recipeViewModel.Recipe.Id);
                Assert.Null(categoryDeleted);
            }
            [Fact]
            public async Task DeleteRecipe_Had1Category_ShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CategorySelector.ItemIds = categories[0].Id.ToString();                
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var categoryNotDeleted = await _unitOfWork.RecipeCategory.GetFirstOrDefaultAsync(x => x.RecipeId == recipeViewModel.Recipe.Id);
                Assert.NotNull(categoryNotDeleted);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var categoryDeleted = await _unitOfWork.RecipeCategory.GetFirstOrDefaultAsync(x => x.RecipeId == recipeViewModel.Recipe.Id);
                Assert.Null(categoryDeleted);
            }

            [Fact]
            public async Task DeleteRecipe_Had2Cookbooks_ShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);

                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();
                recipeViewModel.CookbookSelector.ItemIds += "," + cookbooks[1].Id.ToString();
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var cookbookNotDeleted = await _unitOfWork.CookbookRecipe.GetFirstOrDefaultAsync(x => x.RecipeId == recipeViewModel.Recipe.Id);
                Assert.NotNull(cookbookNotDeleted);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var cookbookDeleted = await _unitOfWork.CookbookRecipe.GetFirstOrDefaultAsync(x => x.RecipeId == recipeViewModel.Recipe.Id);
                Assert.Null(cookbookDeleted);
            }
            [Fact]
            public async Task DeleteRecipe_Had1Cookbook_ShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);
                recipeViewModel.CookbookSelector.ItemIds = cookbooks[0].Id.ToString();                
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var cookbookNotDeleted = await _unitOfWork.CookbookRecipe.GetFirstOrDefaultAsync(x => x.RecipeId == recipeViewModel.Recipe.Id);
                Assert.NotNull(cookbookNotDeleted);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var cookbookDeleted = await _unitOfWork.CookbookRecipe.GetFirstOrDefaultAsync(x => x.RecipeId == recipeViewModel.Recipe.Id);
                Assert.Null(cookbookDeleted);
            }

            [Fact]
            public async Task DeleteRecipe_HadOwner_ShouldBeNull()
            {
                string currentUserId = applicationUsers[0].Id;
                RecipeViewModel recipeViewModel = GetRecipeViewModel(communities[0]);                
                await _recipeOrchestrator.AddAsync(currentUserId, recipeViewModel);
                var applicationUserRecipeNotDeleted = await _unitOfWork.ApplicationUserRecipe.GetFirstOrDefaultAsync(x => x.ObjectId == recipeViewModel.Recipe.Id);
                Assert.NotNull(applicationUserRecipeNotDeleted);
                await _recipeOrchestrator.DeleteAsync(currentUserId, recipeViewModel.Recipe);
                var applicationUserRecipeDeleted = await _unitOfWork.ApplicationUserRecipe.GetFirstOrDefaultAsync(x => x.ObjectId == recipeViewModel.Recipe.Id);
                Assert.Null(applicationUserRecipeDeleted);
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

            recipeViewModel.IngredientText = @"1 lb ground beef
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

            recipeViewModel.InstructionText = @"In a medium pot, saute onions until soft.
Add beef, cook until brown.
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
            Community community2 = new Community() { Name = "SANTA MARIA", Active = true, CountryId = country.Id };
            unitOfWork.Community.Add(community2);
            communities.Add(community2);
            unitOfWork.CommunityState.AddFromEntities(community2, state);

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
            Cookbook cookbook4 = new Cookbook()
            {
                Author = "Ryan Wemmer",
                Copyright = "2017",
                Name = "Just Cookies",
                Description = "Ryan's Best Cookies",
                Privacy = Models.Enums.Privacy.Public
            };
            
            unitOfWork.Cookbook.Add(cookbook4);
            unitOfWork.Save();
            cookbooks.Add(cookbook1);
            cookbooks.Add(cookbook2);
            cookbooks.Add(cookbook3);
            cookbooks.Add(cookbook4);

            unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook1);
            unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook2);
            unitOfWork.ApplicationUserCookbook.AddFromEntities(user2, cookbook3);
            unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook4);
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
            Category category3 = new Category()
            {
                Name = "Quick and Easy",
                DisplayOrder = 2
            };
            unitOfWork.Category.Add(category3);
            unitOfWork.Save();            
            categories.Add(category1);
            categories.Add(category2);
            categories.Add(category3);
        }
        #endregion
    }
}
