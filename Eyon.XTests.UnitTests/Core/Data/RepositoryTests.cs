using Xunit;
using System;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;

namespace Eyon.XTests.UnitTests.Core.Data
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
