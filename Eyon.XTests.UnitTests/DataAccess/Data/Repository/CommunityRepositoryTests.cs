using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Eyon.Models;
namespace Eyon.XTests.UnitTests.DataAccess.Data.Repository
{
    public class CommunityRepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;

        public CommunityRepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CategoryRepositoryTests));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void AddCommunity_WhenEmpty_CommunityIdGreaterThan0()
        {
            var community = new Community()
            {
                Name = "Quincy",
                WikipediaURL = "https://en.wikipedia.org/wiki/Quincy,_California",
                County = "Plumas",
                Active = true
            };
            _unitOfWork.Community.Add(community);
            _unitOfWork.Save();
            Assert.True(community.Id > 0);
        }
        //[Fact]
        //public void AddCommunnity_WhenWikipediaURLIsDuplicate_ThrowsException()
        //{
        //    var community1 = new Community()
        //    {
        //        Name = "Quincy",
        //        WikipediaURL = "https://en.wikipedia.org/wiki/Quincy,_California",
        //        County = "Plumas",
        //        Active = true
        //    };
        //    _unitOfWork.Community.Add(community1);
        //    _unitOfWork.Save();


        //    var community2 = new Community()
        //    {
        //        Name = "Quincy",
        //        WikipediaURL = "https://en.wikipedia.org/wiki/Quincy,_California",
        //        County = "Plumas",
        //        Active = true
        //    };            
        //    _unitOfWork.Community.Add(community2);
        //    Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => _unitOfWork.Save());                
        //    //Assert.ThrowsAny<Exception>(() => _unitOfWork.Save());

        //}
    }
}