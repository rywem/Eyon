using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository
{
    public class RecipeRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;

        public RecipeRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(RecipeRepositoryTests));
        }

        [Fact]
        public void AddRecipe_AssertRecipeAdded_IdGreaterThan0()
        {
            var recipe = new Recipe()
            {
                Name = "Ryan's cookies",
                Cooktime = "20 minutes"
            };

            _unitOfWork.Recipe.Add(recipe);
            _unitOfWork.Save();
            Assert.True(recipe.Id > 0);
        }
        [Fact]
        public void GetRecipe_WhenRecipeExists_ObjPropertiesAreEqual()
        {
            var recipe = new Recipe()
            {
                Name = "Ryan's cookies",
                Cooktime = "20 minutes"
            };

            _unitOfWork.Recipe.Add(recipe);
            _unitOfWork.Save();

            var objFromDb = _unitOfWork.Recipe.Get(recipe.Id);

            Assert.Equal(recipe.Name, objFromDb.Name);
            Assert.Equal(recipe.Cooktime, objFromDb.Cooktime);
        }

        [Theory, InlineData("Ryan's Cheesy Bread", "30 minutes"), 
            InlineData("Ryan's Very Cheesy Bread", "31 minutes")]
        public void UpdateRecipe_WhenRecipeExists_ObjPropertiesAreEqual(string newName, string newCooktime)
        {
            string startingName = "Ryan's Cheese Bread";
            string startingCooktime = "29 minutes";
            var recipe = new Recipe()
            {
                Name = startingName,
                Cooktime = startingCooktime
            };

            _unitOfWork.Recipe.Add(recipe);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Recipe.Get(recipe.Id);

            objFromDb.Name = newName;
            objFromDb.Cooktime = newCooktime;
            _unitOfWork.Recipe.Update(objFromDb);
            _unitOfWork.Save();
            var objFromDbUpdated = _unitOfWork.Recipe.Get(recipe.Id);

            Assert.Equal(newName, objFromDbUpdated.Name);
            Assert.Equal(newCooktime, objFromDbUpdated.Cooktime);
        }
        [Fact]
        public void DeleteRecipeById_WhenRecipeExists_ObjFromDbShouldBeNull(  )
        {
            string startingName = "Ryan's Cheese Bread";
            string startingCooktime = "29 minutes";
            var recipe = new Recipe()
            {
                Name = startingName,
                Cooktime = startingCooktime
            };

            _unitOfWork.Recipe.Add(recipe);
            _unitOfWork.Save();

            _unitOfWork.Recipe.Remove(recipe.Id);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Recipe.Get(recipe.Id);

            Assert.Null(objFromDb);
        }
        [Fact]
        public void DeleteRecipeByEntity_WhenRecipeExists_ObjFromDbShouldBeNull( )
        {
            string startingName = "Ryan's Cheese Bread";
            string startingCooktime = "29 minutes";
            var recipe = new Recipe()
            {
                Name = startingName,
                Cooktime = startingCooktime
            };

            _unitOfWork.Recipe.Add(recipe);
            _unitOfWork.Save();

            _unitOfWork.Recipe.Remove(recipe);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Recipe.Get(recipe.Id);

            Assert.Null(objFromDb);
        }



        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
