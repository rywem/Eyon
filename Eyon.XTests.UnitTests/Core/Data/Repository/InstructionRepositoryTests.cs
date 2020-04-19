using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Eyon.XTests.UnitTests.Core.Data.Repository
{
    public class InstructionRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;

        public InstructionRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(InstructionRepositoryTests));
        }

        [Fact]
        public void AddInstruction_AssertInstructionAdded_IdGreaterThan0()
        {
            var newEntity = new Instruction()
            {
                Text = "Boil water",
                Count = 1
            };
            _unitOfWork.Instruction.Add(newEntity);
            _unitOfWork.Save();
            Assert.True(newEntity.Id > 0);
        }

        [Fact]
        public void GetInstruction_WhenInstructionExists_ObjPropertiesAreEqual()
        {
            var newEntity = new Instruction()
            {
                Text = "Boil water",
                Count = 1
            };

            _unitOfWork.Instruction.Add(newEntity);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Instruction.Get(newEntity.Id);
            Assert.Equal(newEntity.Text, objFromDb.Text);
            Assert.Equal(newEntity.Count, objFromDb.Count);
        }

        [Theory, InlineData("Chop Veggies", 2),
            InlineData("Stir", 3)]
        public void UpdateIngredent_WhenInstructionExists_ObjPropertiesAreEqual( string newText, int newStep )
        {
            string startingText = "Boil Water";
            int startingStep = 1;
            var newEntity = new Instruction()
            {
                Text = startingText,
                Count = startingStep
            };

            _unitOfWork.Instruction.Add(newEntity);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Instruction.Get(newEntity.Id);

            objFromDb.Text = newText;
            objFromDb.Count = newStep;
            _unitOfWork.Instruction.Update(objFromDb);
            _unitOfWork.Save();
            var objFromDbUpdated = _unitOfWork.Instruction.Get(newEntity.Id);
            Assert.Equal(newText, objFromDbUpdated.Text);
            Assert.Equal(newStep, objFromDbUpdated.Count);
        }

        [Fact]
        public void DeleteInstructionById_WhenRecipeExists_ObjFromDbShouldBeNull()
        {
            string startingText = "Boil Water";
            int startingStep = 1;
            var newEntity = new Instruction()
            {
                Text = startingText,
                Count = startingStep
            };
            _unitOfWork.Instruction.Add(newEntity);
            _unitOfWork.Save();
            Assert.True(newEntity.Id > 0);
            _unitOfWork.Instruction.Remove(newEntity.Id);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Instruction.Get(newEntity.Id);

            Assert.Null(objFromDb);
        }

        [Fact]
        public void DeleteInstructionByEntity_WhenInstructionExists_ObjFromDbShouldBeNull()
        {
            string startingText = "Boil Water";
            int startingStep = 1;
            var newEntity = new Instruction()
            {
                Text = startingText,
                Count = startingStep
            };

            _unitOfWork.Instruction.Add(newEntity);
            _unitOfWork.Save();
            Assert.True(newEntity.Id > 0);
            _unitOfWork.Instruction.Remove(newEntity.Id);
            _unitOfWork.Save();
            var objFromDb = _unitOfWork.Instruction.Get(newEntity.Id);

            Assert.Null(objFromDb);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}