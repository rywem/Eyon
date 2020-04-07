using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Eyon.DataAccess.Data.Repository;

namespace Eyon.DataAccess.Orchestrators
{
    public class OrganizationOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrganizationOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }
        public OrganizationViewModel CreateOrganizationViewModel()
        {
            return new OrganizationViewModel();
        }

        public OrganizationViewModel GetOrganizationViewModel( long organizationId )
        {
            throw new NotImplementedException();
        }

        public void AddOrganization( OrganizationViewModel organizationViewModel )
        {
            if ( organizationViewModel.Organization.Id != 0 )
                throw new SafeException("Organization already exists.");

            _unitOfWork.Organization.Add(organizationViewModel.Organization);
            _unitOfWork.Save();

            _unitOfWork.Topic.AddFromITopicItem(organizationViewModel.Organization);
            _unitOfWork.Save();
        }

        public void AddOrganizationTransaction( OrganizationViewModel organizationViewModel )
        {
          
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    AddOrganization(organizationViewModel);
                    
                    transaction.Commit();
                }
                catch ( Exception exception )
                {
                    transaction.Rollback();
                    throw exception;
                }
            }
            
        }

        public void UpdateOrganizationTransaction( OrganizationViewModel organizationViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    UpdateOrganization(organizationViewModel);

                    transaction.Commit();
                }
                catch ( Exception exception )
                {
                    transaction.Rollback();
                    throw exception;
                }
            }
        }

        public void UpdateOrganization( OrganizationViewModel organizationViewModel )
        {
            if ( organizationViewModel.Organization.Id == 0 )
                throw new SafeException("Organization not found.");

            _unitOfWork.Organization.Update(organizationViewModel.Organization);
            _unitOfWork.Save();
        }
    }
}
