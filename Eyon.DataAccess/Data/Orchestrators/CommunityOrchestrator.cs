using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using Eyon.DataAccess.Data.Repository;
using Eyon.DataAccess.Data.Repository.Relationship;
namespace Eyon.DataAccess.Data.Orchestrators
{
    public class CommunityOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommunityOrchestrator( IUnitOfWork unitOfWork )
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

        public CommunityViewModel GetCommunityViewModel( long communityId )
        {
            var communityViewModel = CreateCommunityViewModel();
            communityViewModel.Community = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == communityId, includeProperties: "Country,CommunityState,CommunityState.State");
            if ( communityViewModel.Community != null )
            {
                if ( communityViewModel.Community.CommunityState != null && communityViewModel.Community.CommunityState.StateId != 0 )
                    communityViewModel.StateId = communityViewModel.Community.CommunityState.StateId;
            }
            return communityViewModel;
        }

        public void AddCommunity( CommunityViewModel communityViewModel )
        {
            if ( communityViewModel.Community.Id != 0 )
                throw new WebUserSafeException("Community already exists");
            if ( communityViewModel.Community.CountryId == 0 )
                throw new WebUserSafeException("Country required.");

            _unitOfWork.Community.Add(communityViewModel.Community);
            _unitOfWork.Save();
            var hasStates = _unitOfWork.State.Any(x => x.CountryId == communityViewModel.Community.CountryId);

            if ( hasStates )
            {
                CreateStateRelationship(communityViewModel);
            }
        }


        public void AddCommunityTransaction( CommunityViewModel communityViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    AddCommunity(communityViewModel);
                    // Todo, add community
                    transaction.Commit();
                }
                catch ( Exception safeException )
                {
                    transaction.Rollback();
                    throw safeException;
                }
            }
        }
        public void UpdateCommunityTransaction( CommunityViewModel communityViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    UpdateCommunity(communityViewModel);
                    // Todo, add community
                    transaction.Commit();
                }
                catch ( Exception safeException )
                {
                    transaction.Rollback();
                    throw safeException;
                }
            }
        }
        public void UpdateCommunity(CommunityViewModel communityViewModel )
        {
            if ( _unitOfWork.Community.Any(x => x.Id == communityViewModel.Community.Id) == false )
                throw new WebUserSafeException("Community does not exists");
            
            //Disallow updates to country at this time.
            var communityFromDb = _unitOfWork.Community.Get(communityViewModel.Community.Id);
            if ( communityViewModel.Community.CountryId != 0 )
            {
                if ( communityViewModel.Community.CountryId != communityFromDb.CountryId )
                    throw new WebUserSafeException("Changing country disallowed.");
            }
            else
                communityViewModel.Community.CountryId = communityFromDb.CountryId;


            _unitOfWork.Community.Update(communityViewModel.Community);


            // At this time, do not allow updates to states or country. 
            //var hasStates = _unitOfWork.State.Any(x => x.CountryId == communityViewModel.Community.CountryId);
            //if ( hasStates )
            //{
            //    if ( communityViewModel.Community.CommunityState != null )
            //    {
            //        if ( communityViewModel.Community.CommunityState.StateId == communityViewModel.StateId.GetValueOrDefault() || communityViewModel.StateId.GetValueOrDefault() == 0 )
            //            return;
            //        else
            //        {
            //            _unitOfWork.CommunityState.Remove(communityViewModel.Community.CommunityState);
            //            _unitOfWork.Save();
            //        }
            //    }
            //    if ( communityViewModel.Community.CommunityState.StateId != communityViewModel.StateId.GetValueOrDefault() && communityViewModel.StateId != 0 )
            //    {
            //        CreateStateRelationship(communityViewModel);
            //    }
            //}


        }
        private void CreateStateRelationship( CommunityViewModel communityViewModel )
        {           
            if ( communityViewModel.StateId.GetValueOrDefault() == 0 )
                throw new WebUserSafeException("State/Province required.");

            var state = _unitOfWork.State.GetFirstOrDefault(x => x.CountryId == communityViewModel.Community.CountryId
                                                            && x.Id == communityViewModel.StateId.GetValueOrDefault());
            if ( state == null || state.Id == 0 )
                throw new WebUserSafeException("Invalid State selected.");

            var communityState = new Models.Relationship.CommunityState()
            {
                CommunityId = communityViewModel.Community.Id,
                StateId = state.Id
            };
            _unitOfWork.CommunityState.Add(communityState);
            _unitOfWork.Save();            
        }
    }

}
