using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Orchestrators
{
    public class CommunityOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommunityOrchestrator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public CommunityViewModel CreateCommunityViewModel()
        {
            CommunityViewModel communityViewModel = new CommunityViewModel();            
            communityViewModel.CountryList = _unitOfWork.Country.GetCountryListForDropDown();
            communityViewModel.StateList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            return communityViewModel;
        }

        public CommunityViewModel GetCommunityViewModel(long communityId)
        {
            var communityViewModel = CreateCommunityViewModel();
            communityViewModel.Community = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == communityId, includeProperties: "Country,CommunityState");
            if (communityViewModel.Community != null)
            {
                if (communityViewModel.Community.CommunityState != null && communityViewModel.Community.CommunityState.StateId != 0)
                    communityViewModel.StateId = communityViewModel.Community.CommunityState.StateId;
            }
            return communityViewModel;
        }
    }
}
