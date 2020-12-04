using Eyon.Core.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.Core.Orchestrators
{
    public class StateOrchestrator
    {

        private readonly IUnitOfWork _unitOfWork;

        public StateOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task RunSync()
        {
            var states = await _unitOfWork.State.GetAllAsync();
            foreach ( var state in states)
            {
                if ( _unitOfWork.Topic.Any(x => x.ObjectId == state.Id && x.TopicType == state.TopicType) )
                    continue;

                _unitOfWork.Topic.AddFromITopicItem(state);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
