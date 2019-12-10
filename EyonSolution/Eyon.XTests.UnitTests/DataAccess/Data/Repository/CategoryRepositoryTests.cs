using Xunit;
using Eyon.DataAccess.Data.Repository;
using Eyon.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Eyon.DataAccess.Data;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository
{

    public class CategoryRepositoryTests
    {
        IUnitOfWork _unitOfWork = GetInMemoryUnitOfWork();        
        [Fact]
        public void AddRepository_AssertCanCategoryAdded()
        {            
            //Setup
            var category = new Models.Category()
            {
                DisplayOrder = 1,
                Name = "Test Category"                
            };
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            Assert.True(category.Id > 0);
        }

        [Fact]
        public void AddRepository_AssertCanGetCategoryFromDb()
        {
            //Setup            
            var category = new Models.Category()
            {
                DisplayOrder = 1,
                Name = "Test Category"
            };
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();

            var id = category.Id;
            var categoryFromDb = _unitOfWork.Category.Get(id);

            Assert.Equal(category.Name, categoryFromDb.Name);
            Assert.Equal(category.Id, categoryFromDb.Id);
        }
        [Fact]
        public void AddRepository_AssertCanUpdateCategoryInDb()
        {
            //Setup
            var firstName = "Test Category";
            var firstDisplayOrder = 1;
            var category = new Models.Category()
            {
                DisplayOrder = firstDisplayOrder,
                Name = firstName
            };
            var id = category.Id;
            _unitOfWork.Category.Add(category);
            Assert.Equal(category.Name, firstName);
            Assert.Equal(category.DisplayOrder, firstDisplayOrder);
            Assert.True(category.Id > 0);
            var currentId = category.Id;
            var secondName = "New Name";
            var secondDisplayOrder = 2;
            category.Name = secondName;
            category.DisplayOrder = secondDisplayOrder;
            _unitOfWork.Category.Add(category);
            var categoryFromDb = _unitOfWork.Category.Get(currentId);
            Assert.Equal(categoryFromDb.Name, secondName);
            Assert.Equal(categoryFromDb.DisplayOrder, secondDisplayOrder);
            Assert.Equal(category.Id, currentId);
        }

        //https://www.carlrippon.com/testing-ef-core-repositories-with-xunit-and-an-in-memory-db/
        private static IUnitOfWork GetInMemoryUnitOfWork()
        {            
            DbContextOptions<Eyon.DataAccess.Data.ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<Eyon.DataAccess.Data.ApplicationDbContext>();
            builder.UseInMemoryDatabase("Eyon_Test");
            options = builder.Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);            
            dbContext.Database.EnsureCreated();
            dbContext.Database.EnsureDeleted();
            return new UnitOfWork(dbContext);
            
        }
    }
}
