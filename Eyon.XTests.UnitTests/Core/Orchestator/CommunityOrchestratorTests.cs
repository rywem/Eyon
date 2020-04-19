using Eyon.Core.Orchestrators;
using Eyon.Core.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.XTests.UnitTests.Core.Orchestator
{
    public class CommunityOrchestratorTests
    {
        IUnitOfWork _unitOfWork;
        CommunityOrchestrator _orchestrator;
        public CommunityOrchestratorTests()
        {
            this._unitOfWork = new Resources().GetInMemoryUnitOfWork(nameof(CommunityOrchestratorTests));
            this._orchestrator = new Eyon.Core.Orchestrators.CommunityOrchestrator(_unitOfWork);
            SeedDatabase();
        }

        private void SeedDatabase()
        {

        }
    }
}
