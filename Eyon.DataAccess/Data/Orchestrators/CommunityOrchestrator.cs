using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using Eyon.DataAccess.Data.Repository;
using System.Threading.Tasks;
using Eyon.Models;
using Eyon.Models.Relationship;
using System.Linq;
using Eyon.DataAccess.SeedData.Location;

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

        private async Task AddZipcode( ZipCodeFile zip, Country country)
        {
            string city = zip.City.ToUpper();
            string state = zip.State.ToUpper();

            var stateFromDb = await _unitOfWork.State.GetFirstOrDefaultAsync(x => x.CountryId == country.Id && x.Code == state);

            if ( stateFromDb != null )
            {
                Community community = null;
                var communities = await _unitOfWork.Community.GetAllAsync(x => x.CountryId == country.Id && x.Name == city, includeProperties: "CommunityState,CommunityState.State,CommunityPostalCode,CommunityPostalCode.PostalCode,CommunityGeocode,CommunityGeocode.Geocode");

                if ( communities != null )
                {
                    community = communities.ToList().Where(x => x.CommunityState.State.Code.Equals(state)).FirstOrDefault();
                }
                if ( community == null )
                {
                    community = new Community()
                    {
                        Active = true,
                        CountryId = country.Id,
                        Name = city,
                    };
                    _unitOfWork.Community.Add(community);
                    await _unitOfWork.SaveAsync();
                }
                
                // Create state relationship
                if ( community.CommunityState == null )
                {
                    CommunityState communityState = new CommunityState()
                    {
                        CommunityId = community.Id,
                        StateId = stateFromDb.Id
                    };
                    _unitOfWork.CommunityState.Add(communityState);
                    await _unitOfWork.SaveAsync();
                }

                PostalCode postal = null;
                CommunityPostalCode commmunityPostalCode = null;
                var zipText = zip.Zipcode.ToUpper();
                if ( community.CommunityPostalCode != null )
                {                    
                    postal = community.CommunityPostalCode.ToList().Select(x => x.PostalCode).Where(x => x.Text.Equals(zipText)).FirstOrDefault();
                }
                
                //postal = community.CommunityPostalCodes.ToList().Select(x => x.PostalCode).Where(x => x.Text.Equals(zipText)).FirstOrDefault();
                if ( postal != null )
                    commmunityPostalCode = community.CommunityPostalCode.FirstOrDefault(x => x.PostalCodeId == postal.Id);
                else
                    postal = await _unitOfWork.PostalCode.GetFirstOrDefaultAsync(x => x.Text.Equals(zipText) && x.CountryId == country.Id);

                // create postal code.
                if ( postal == null )
                {
                    postal = new PostalCode()
                    {
                        CountryId = country.Id,
                        Text = zip.Zipcode.ToUpper(),
                    };
                    _unitOfWork.PostalCode.Add(postal);
                    await _unitOfWork.SaveAsync();
                }
                
                if ( commmunityPostalCode == null )                
                {
                    commmunityPostalCode = new CommunityPostalCode()
                    {
                        CommunityId = community.Id,
                        PostalCodeId = postal.Id
                    };
                    _unitOfWork.CommunityPostalCode.Add(commmunityPostalCode);
                    await _unitOfWork.SaveAsync();
                }
                // check for geo code 
                Geocode geocode = null;
                CommunityGeocode communityGeocode = null;
                string lati = zip.Lat.ToUpper();
                string longi = zip.Long.ToUpper();
                if ( community.CommunityGeocode != null )
                {
                    // check if the postal code exists:
                    //geocode = await _unitOfWork.Geocode.GetFirstOrDefaultAsync(x => x.Latitude.Equals(lati) && x.Longitude.Equals(longi));
                    geocode = community.CommunityGeocode.ToList().Select(x => x.Geocode).Where(x => x.Latitude.Equals(lati) && x.Longitude.Equals(longi)).FirstOrDefault();
                }
                                    
                if ( geocode != null )
                    communityGeocode = community.CommunityGeocode.FirstOrDefault(x => x.GeocodeId == geocode.Id);
                else
                    geocode = await _unitOfWork.Geocode.GetFirstOrDefaultAsync(x => x.Latitude.Equals(lati) && x.Longitude.Equals(longi));
                

                if ( geocode == null )
                {
                    geocode = new Geocode()
                    {
                        Latitude = lati,
                        Longitude = longi
                    };
                    _unitOfWork.Geocode.Add(geocode);
                    await _unitOfWork.SaveAsync();
                }
                if (communityGeocode == null )
                {
                    communityGeocode = new CommunityGeocode()
                    {
                        CommunityId = community.Id,
                        GeocodeId = geocode.Id
                    };
                    _unitOfWork.CommunityGeocode.Add(communityGeocode);
                    await _unitOfWork.SaveAsync();
                }

                // last, check if the postal geocode relation exists.
                PostalCodeGeocode postalCodeGeocode = await  _unitOfWork.PostalCodeGeocode.GetFirstOrDefaultAsync(x => x.GeocodeId == geocode.Id && x.PostalCodeId == postal.Id);

                if(postalCodeGeocode == null)
                {
                    postalCodeGeocode = new PostalCodeGeocode() { GeocodeId = geocode.Id, PostalCodeId = postal.Id };
                    _unitOfWork.PostalCodeGeocode.Add(postalCodeGeocode);
                    await _unitOfWork.SaveAsync();
                }
            }
        }
        private async Task AddZipcodeTransaction( ZipCodeFile zip, Country country )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await AddZipcode(zip, country);
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

        public async Task UploadCommunities(List<ZipCodeFile> zipcodes, long countryId)
        {

            // loop through the list of zip codes
            var countryFromDb = await _unitOfWork.Country.GetFirstOrDefaultAsync(x => x.Id == countryId);

            if ( countryFromDb != null ) {
                foreach ( var zip in zipcodes )
                {
                    await AddZipcodeTransaction(zip, countryFromDb);                    
                }
            }
        }

        public void AddCommunity( CommunityViewModel communityViewModel )
        {
            if ( communityViewModel.Community.Id != 0 )
                throw new SafeException("Community already exists");
            if ( communityViewModel.Community.CountryId == 0 )
                throw new SafeException("Country required.");

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
                throw new SafeException("Community does not exists");
            
            //Disallow updates to country at this time.
            var communityFromDb = _unitOfWork.Community.Get(communityViewModel.Community.Id);
            if ( communityViewModel.Community.CountryId != 0 )
            {
                if ( communityViewModel.Community.CountryId != communityFromDb.CountryId )
                    throw new SafeException("Changing country disallowed.");
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
                throw new SafeException("State/Province required.");

            var state = _unitOfWork.State.GetFirstOrDefault(x => x.CountryId == communityViewModel.Community.CountryId
                                                            && x.Id == communityViewModel.StateId.GetValueOrDefault());
            if ( state == null || state.Id == 0 )
                throw new SafeException("Invalid State selected.");

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
