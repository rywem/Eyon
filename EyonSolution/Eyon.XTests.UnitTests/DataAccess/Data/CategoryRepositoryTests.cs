using Xunit;
using Eyon.DataAccess.Data.Repository;
using Eyon.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Eyon.DataAccess.Data;

namespace Eyon.XTests.UnitTests.DataAccess.Data
{

    public class CategoryRepositoryTests
    {
        ApplicationDbContext dbContext = GetInMemoryContext();        
        [Fact]
        public void AddRepository_AssertCanCategoryAdded()
        {
            ICategoryRepository categoryRepository =  new CategoryRepository(dbContext);
            var category = new Models.Category()
            {
                DisplayOrder = 1,
                Name = "Test Category"                
            };
            categoryRepository.Add(category);
            dbContext.SaveChanges();
            Assert.Equal(1, category.Id);            
        }

        //https://www.carlrippon.com/testing-ef-core-repositories-with-xunit-and-an-in-memory-db/
        private static ApplicationDbContext GetInMemoryContext()
        {            
            DbContextOptions<Eyon.DataAccess.Data.ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<Eyon.DataAccess.Data.ApplicationDbContext>();
            builder.UseInMemoryDatabase("Eyon_Test");
            options = builder.Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);            
            dbContext.Database.EnsureCreated();
            dbContext.Database.EnsureDeleted();
            return dbContext;
        }
    }
}
