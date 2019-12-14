using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository
{
    public class CountryRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
       
        public CountryRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CountryRepositoryTests));
        }
        [Fact]
        public void AddCountry_CreateNewCountry_AssertIdGreaterThan0()
        {
            // arrange
            var country = new Models.Country() { Name = "UNITED STATES", Code = "US" };
            // act
            _unitOfWork.Country.Add(country);
            // assert
            Assert.True(country.Id > 0);
        }

        [Fact]
        public void GetCountry_WhenCountryExists_AssertIdGreaterThan0()
        {
            // arrange
            var country = new Models.Country() { Name = "UNITED STATES", Code = "US" };
            _unitOfWork.Country.Add(country);
            // act
            var countryFromDb = _unitOfWork.Country.Get(country.Id);

            // assert
            Assert.Equal(country.Name, countryFromDb.Name);
            Assert.Equal(country.Code, countryFromDb.Code);
            Assert.Equal(country.Id, countryFromDb.Id);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
