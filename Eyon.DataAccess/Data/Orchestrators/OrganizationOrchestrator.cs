using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Orchestrators
{
    public class OrganizationOrchestrator
    {

        public OrganizationViewModel CreateOrganizationViewModel()
        {
            return new OrganizationViewModel();
        }

        public OrganizationViewModel GetOrganizationViewModel( long organizationId )
        {
            throw new NotImplementedException();
        }
    }
}
