using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository
{
    public class IngredientRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;

        public IngredientRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(IngredientRepositoryTests));
        }

        [Fact]
        public void AddIngredient_AssertIngredientAdded_IdGreaterThan0()
        {
            var newEntity = new Ingredient()
            {
                Text = "Broccoli",                
            };
            _unitOfWork.Ingredient.Add(newEntity);
            _unitOfWork.Save();
            Assert.True(newEntity.Id > 0);
        }

        [Fact]
        public void GetIngredient_WhenIngredientExists_ObjPropertiesAreEqual()
        {
            var newEntity = new Ingredient()
            {
                Text = "Broccoli",
            };

            _unitOfWork.Ingredient.Add(newEntity);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Ingredient.Get(newEntity.Id);
            Assert.Equal(newEntity.Text, objFromDb.Text);            
        }

        [Theory, InlineData("Green Broccoli"),
            InlineData("Small Broccoli")]
        public void UpdateIngredent_WhenIngredientExists_ObjPropertiesAreEqual( string newName )
        {
            string startingName = "Broccoli";            
            var newEntity = new Ingredient()
            {
                Text = startingName,                
            };

            _unitOfWork.Ingredient.Add(newEntity);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Ingredient.Get(newEntity.Id);

            objFromDb.Text = newName;            
            _unitOfWork.Ingredient.Update(objFromDb);
            _unitOfWork.Save();
            var objFromDbUpdated = _unitOfWork.Ingredient.Get(newEntity.Id);
            Assert.Equal(newName, objFromDbUpdated.Text);
        }

        [Fact]
        public void DeleteIngredientById_WhenRecipeExists_ObjFromDbShouldBeNull()
        {
            string startingName = "Broccoli";
            var newEntity = new Ingredient()
            {
                Text = startingName,
            };
            _unitOfWork.Ingredient.Add(newEntity);
            _unitOfWork.Save();
            Assert.True(newEntity.Id > 0);
            _unitOfWork.Ingredient.Remove(newEntity.Id);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Ingredient.Get(newEntity.Id);

            Assert.Null(objFromDb);
        }

        [Fact]
        public void DeleteIngredientByEntity_WhenIngredientExists_ObjFromDbShouldBeNull()
        {
            string startingName = "Broccoli";
            var newEntity = new Ingredient()
            {
                Text = startingName,
            };
            _unitOfWork.Ingredient.Add(newEntity);
            _unitOfWork.Save();
            Assert.True(newEntity.Id > 0);
            _unitOfWork.Ingredient.Remove(newEntity.Id);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Ingredient.Get(newEntity.Id);

            Assert.Null(objFromDb);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}