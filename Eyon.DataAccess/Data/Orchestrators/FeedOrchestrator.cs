using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Orchestrators
{
    public class FeedOrchestrator
    {

        private readonly IUnitOfWork _unitOfWork;
        public FeedOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }
        public FeedViewModel GetPublicFeedViewModel()
        {
            throw new NotImplementedException();
            FeedViewModel feedViewModel = new FeedViewModel();
            //feedViewModel.Feed = _unitOfWork.Feed.Get
            return feedViewModel;
        }
    }
}
