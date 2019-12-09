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
        ICategoryRepository categoryRepository = GetInMemoryRepository();
        [Fact]
        public void AddRepository_AssertCategoryAdded()
        {

        }

        //https://www.carlrippon.com/testing-ef-core-repositories-with-xunit-and-an-in-memory-db/
        private ICategoryRepository GetInMemoryRepository()
        {            
            DbContextOptions<Eyon.DataAccess.Data.ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<Eyon.DataAccess.Data.ApplicationDbContext>();
            builder.UseInMemoryDatabase("Eyon_Test");
            options = builder.Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);            
            dbContext.Database.EnsureCreated();
            dbContext.Database.EnsureDeleted();
            return new CategoryRepository(dbContext);
        }
    }
}
