using Xunit;
using System;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.DataAccess.Data
{
    public class RepositoryTests : IDisposable
    {
        IUnitOfWork _unitOfWork;
        public RepositoryTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(RepositoryTests));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
