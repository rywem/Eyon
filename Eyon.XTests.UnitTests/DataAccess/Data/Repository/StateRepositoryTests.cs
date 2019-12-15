using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository
{
    public class StateRepositoryTests
    {
        IUnitOfWork _unitOfWork;

        public StateRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(StateRepositoryTests));
        }

        [Fact]
        public void GetState_WhenStateAndCountryExists_AssertPropertiesEqual()
        {
            // arrange
            var country = new Models.Country() { Name = "UNITED STATES", Code = "US" };
            _unitOfWork.Country.Add(country);
            _unitOfWork.Save();
            var state = new Models.State() { Name = "Connecticut", Code = "CT", CountryId = country.Id, LocalName = "Connecticut", Type = "State" };
            _unitOfWork.State.Add(state);
            _unitOfWork.Save();

            // act 
            var stateFromDb = _unitOfWork.State.Get(state.Id);
            // assert
            Assert.Equal(state.Id, stateFromDb.Id);
            Assert.Equal(state.Name, stateFromDb.Name);
            Assert.Equal(state.LocalName, stateFromDb.LocalName);
            Assert.Equal(state.Code, stateFromDb.Code);
            Assert.Equal(state.Type, stateFromDb.Type);
        }

        [Fact]
        public void GetStateByCountryId_WhenStateAndCountryExists_AssertCountIsGreaterThan0()
        {
            // arrange
            var country = new Models.Country() { Name = "UNITED STATES", Code = "US" };
            _unitOfWork.Country.Add(country);
            _unitOfWork.Save();
            var state = new Models.State() { Name = "Connecticut", Code = "CT", CountryId = country.Id, LocalName = "Connecticut", Type = "State" };
            _unitOfWork.State.Add(state);
            _unitOfWork.Save();

            // act 
            var statesFromDb = _unitOfWork.State.GetAll(x => x.CountryId == country.Id).ToList();
            // assert
            Assert.True(statesFromDb.Count > 0);
            
        }
    }
}
