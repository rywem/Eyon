﻿using Xunit;
using Eyon.Core.Data.Repository;
using Eyon.Core.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Eyon.Core.Data;
using System;

namespace Eyon.XTests.UnitTests.Core.Data.Repository
{

    public class CategoryRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;

        public CategoryRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CategoryRepositoryTests));
        }

        [Fact]
        public void AddCategory_AssertCategoryAdded_IdGreaterThan0()
        {            
            //Setup
            var category = new Models.Category()
            {
                Name = "Test Category"                
            };
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            Assert.True(category.Id > 0);
        }

        [Fact]
        public void GetCategory_WhenCategoryExists_ObjPropertiesAreEqual()
        {
            // arrange           
            var category = new Models.Category()
            {
                Name = "Test Category"
            };
            
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            // act
            var id = category.Id;
            var categoryFromDb = _unitOfWork.Category.Get(id);

            // assert
            Assert.Equal(category.Name, categoryFromDb.Name);
            Assert.Equal(category.Id, categoryFromDb.Id);
        }
        [Fact]
        public void UpdateCategory_WhenCategoryUpdated_CategoryHasNewValues()
        {
            //Setup
            var firstName = "Test Category";
            var firstDisplayOrder = 1;
            var category = new Models.Category()
            {
                Name = firstName
            };
            var id = category.Id;
            _unitOfWork.Category.Add(category);
            Assert.Equal(category.Name, firstName);
            Assert.True(category.Id > 0);
            var currentId = category.Id;
            var secondName = "New Name";
            category.Name = secondName;
            _unitOfWork.Category.Add(category);
            var categoryFromDb = _unitOfWork.Category.Get(currentId);
            Assert.Equal(categoryFromDb.Name, secondName);            
            Assert.Equal(category.Id, currentId);
        }
        [Fact]
        public void DeleteCategory_WhenCategoryDeleted_DbObjIsNull()
        {
            // arrange           
            var category = new Models.Category()
            {                
                Name = "Test Category"
            };

            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            var id = category.Id;
            var categoryFromDb = _unitOfWork.Category.Get(id);
            Assert.True(categoryFromDb.Id > 0);
            // act
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            categoryFromDb = _unitOfWork.Category.Get(id);

            // assert            
            Assert.Null(categoryFromDb);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
