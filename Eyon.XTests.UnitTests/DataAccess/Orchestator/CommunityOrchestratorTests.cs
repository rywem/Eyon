using Eyon.DataAccess.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.XTests.UnitTests.DataAccess.Orchestator
{
    public class CommunityOrchestratorTests
    {
        IUnitOfWork _unitOfWork;
        CommunityOrchestrator _orchestrator;
        public CommunityOrchestratorTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CommunityOrchestratorTests));
            this._orchestrator = new Eyon.DataAccess.Orchestrators.CommunityOrchestrator(_unitOfWork);
            SeedDatabase();
        }

        private void SeedDatabase()
        {

        }
    }
}
