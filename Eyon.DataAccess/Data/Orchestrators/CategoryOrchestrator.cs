using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Orchestrators
{
    public class CategoryOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task RunSync()
        {
            var categories = await _unitOfWork.Category.GetAllAsync();
            foreach ( var category in categories )
            {
                //if ( state.FeedState != null && state.FeedState.Count > 0 )
                //    continue;
                if ( _unitOfWork.Topic.Any(x => x.ObjectId == category.Id && x.TopicType == category.TopicType) )
                    continue;

                _unitOfWork.Topic.AddFromITopicItem(category);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
