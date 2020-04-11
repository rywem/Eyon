using Xunit;
using Eyon.DataAccess.Data.Repository;
using Eyon.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Eyon.DataAccess.Data;
using System;
using Eyon.Models.ViewModels;
using Eyon.DataAccess.Orchestrators;
using System.Linq;
using Eyon.DataAccess.DataCalls;

namespace Eyon.XTests.UnitTests.DataAccess.Orchestator
{
    public class CookbookOrchestratorTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        CookbookOrchestrator _orchestrator;
        public CookbookOrchestratorTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CookbookOrchestratorTests));
            this._orchestrator = new Eyon.DataAccess.Orchestrators.CookbookOrchestrator(_unitOfWork, new FeedDataCall(this._unitOfWork));
            SeedDatabase();
        }

        /// <summary>
        /// Seeds database with test data
        /// </summary>
        private void SeedDatabase()
        {            
            // arrange           
            var category = new Models.Category()
            {
                DisplayOrder = 1,
                Name = "Test Category"
            };
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            var category2 = new Models.Category()
            {
                DisplayOrder = 2,
                Name = "Test Category2"
            };
            _unitOfWork.Category.Add(category2);
            _unitOfWork.Save();
            var cookbook = new Models.Cookbook()
            {
                Author = "Ryan",
                Copyright = "2019",
                Description = "My Description",
                ISBN = "123-456",
                Name = "A cookbook",
            };
            // act
            _unitOfWork.Cookbook.Add(cookbook);
            _unitOfWork.Save();            
        }

        [Fact]
        public void NewCookbookViewModel_WhereCookbookWithNoRelationsExists_CookbookIdGreaterThan0()
        {
            // arrange           
            var cookbookInDb = _unitOfWork.Cookbook.Get(1);           
            Assert.NotNull(cookbookInDb);
            // act
            var cookbookViewModel = _orchestrator.Get(cookbookInDb.Id);
            Assert.NotNull(cookbookViewModel);
            Assert.NotNull(cookbookViewModel.Cookbook);
            Assert.True(cookbookViewModel.Cookbook.Id > 0);
        }

        [Fact]
        public void CookbookViewModel_AddCategoryRelationshipToExistingCookbook_CookbookCategoriesCountEqual1()
        {
            // Arrange
            //var cookbookInDb = _unitOfWork.Cookbook.GetFirstOrDefault();
            //var categoryInDb = _unitOfWork.Category.GetFirstOrDefault();
            //var cookbookViewModel = _orchestrator.GetCookbookViewModel(cookbookInDb.Id);
            //cookbookViewModel.CategoryIds = categoryInDb.Id.ToString();
            //// Act
            //_orchestrator.UpdateCookbook(cookbookViewModel);
            //var cookbookCategoriesFromDb = _unitOfWork.CookbookCategory.GetAll(f => f.CookbookId == cookbookInDb.Id && f.CategoryId == categoryInDb.Id).ToList();

            //// Assert
            //Assert.True(cookbookCategoriesFromDb.Count == 1);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
