using Xunit;
using Eyon.DataAccess.Data.Repository;
using Eyon.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Eyon.DataAccess.Data;
using System;
using Eyon.Models.ViewModels;
using Eyon.DataAccess.Data.Orchestrators;
using System.Linq;

namespace Eyon.XTests.UnitTests.DataAccess.Data.Orchestator
{
    public class RecipeOrchestratorTests : IDisposable
    {

        IUnitOfWork _unitOfWork;
        RecipeOrchestrator _orchestrator;
        public RecipeOrchestratorTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(RecipeOrchestratorTests));
            this._orchestrator = new Eyon.DataAccess.Data.Orchestrators.RecipeOrchestrator(_unitOfWork, null);
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
