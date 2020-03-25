using System;
using Xunit;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository.Relationship
{
    public class CookbookCategoryRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public CookbookCategoryRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CookbookCategoryRepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        [Fact]
        public void AddCoookbookCategory_FromEntities()
        {
            var firstEntity = new Cookbook()
            {
                Id = 1
            };
            var secondEntity = new Category()
            {
                Id = 3
            };

            var entityRelation = _unitOfWork.CookbookCategory.AddFromEntities(firstEntity, secondEntity);
            Assert.Equal(firstEntity.Id, entityRelation.CookbookId);
            Assert.Equal(secondEntity.Id, entityRelation.CategoryId);
        }
    }
}
