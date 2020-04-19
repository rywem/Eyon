using Xunit;
using Eyon.Core.Data.Repository;
using Eyon.Core.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Eyon.Core.Data;
using System;


namespace Eyon.XTests.UnitTests.Core.Data.Repository
{
    public class CookbookRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;

        public CookbookRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CookbookRepositoryTests));
        }

        [Fact]
        public void AddCookbook_AssertCookbookAdded_IdGreaterThan0()
        {
            // arrange
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
            // assert
            Assert.True(cookbook.Id > 0);
        }

        [Fact]
        public void GetCookbook_CookbookExists_CookbookObjPropertiesAreEqual()
        {
            // arrange
            var cookbookObj = new Models.Cookbook()
            {
                Author = "Ryan",
                Copyright = "2019",
                Description = "My Description",
                ISBN = "123-456",
                Name = "A cookbook",
            };
            _unitOfWork.Cookbook.Add(cookbookObj);
            _unitOfWork.Save();
            // act
            var id = cookbookObj.Id;
            var objFromDb = _unitOfWork.Cookbook.Get(id);
            Assert.Equal(cookbookObj.ISBN, objFromDb.ISBN);
            Assert.Equal(cookbookObj.Copyright, objFromDb.Copyright);
            Assert.Equal(cookbookObj.Description, objFromDb.Description);
            Assert.Equal(cookbookObj.Author, objFromDb.Author);
            Assert.Equal(cookbookObj.Name, objFromDb.Name);
        }

        [Fact]
        public void UpdateCookbook_CookbookUpdated_CookbookHasNewValues()
        {
            // arrange
            //var name1 = "A cookbook";
            //var copyright1 = "2019";
            //var description1 = "My Description";
            //var isbn1 = "123-456";
            //var author1 = "Ryan";
            //var cookbookObj = new Models.Cookbook()
            //{
            //    Author = author1,
            //    Copyright = copyright1,
            //    Description = description1,
            //    ISBN = isbn1,
            //    Name = name1,
            //};
            //_unitOfWork.Cookbook.Add(cookbookObj);
            //_unitOfWork.Save();
            //Assert.True(cookbookObj.Id > 0);

            //// act 
            //var name2 = "A cookbook";
            //var copyright2 = "2019";
            //var description2 = "My Description";
            //var isbn2 = "123-456";
            //var author2 = "Ryan";
            //cookbookObj.Author = author2;
            //cookbookObj.Copyright = copyright2;
            //cookbookObj.Description = description2;
            //cookbookObj.ISBN = isbn2;
            //cookbookObj.Name = name2;
            //_unitOfWork.Cookbook.Update(cookbookObj);
            //// assert
            //var objFromDb = _unitOfWork.Cookbook.Get(cookbookObj.Id);
            //Assert.Equal(cookbookObj.Name, objFromDb.Name);
            //Assert.Equal(cookbookObj.Author, objFromDb.Author);
            //Assert.Equal(cookbookObj.Copyright, objFromDb.Copyright);
            //Assert.Equal(cookbookObj.Description, objFromDb.Description);
            //Assert.Equal(cookbookObj.ISBN, objFromDb.ISBN);
            //Assert.Equal(cookbookObj.Id, objFromDb.Id);
        }
        [Fact]
        public void DeleteCookbook_WhenCookbookDeleted_DbObjIsNull()
        {
            // arrange           
            // arrange
            var cookbookObj = new Models.Cookbook()
            {
                Author = "Ryan",
                Copyright = "2019",
                Description = "My Description",
                ISBN = "123-456",
                Name = "A cookbook",
            };

            _unitOfWork.Cookbook.Add(cookbookObj);
            _unitOfWork.Save();
            var id = cookbookObj.Id;
            var objFromDb = _unitOfWork.Cookbook.Get(id);
            Assert.True(objFromDb.Id > 0);
            // act
            _unitOfWork.Cookbook.Remove(objFromDb);
            _unitOfWork.Save();
            objFromDb = _unitOfWork.Cookbook.Get(id);

            // assert            
            Assert.Null(objFromDb);
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
