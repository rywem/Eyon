using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class OrganizationViewModel
    {
        public Organization Organization { get; set; }
        public List<Community> Community { get; set; }
        public List<Cookbook> Cookbook { get; set; }

        public OrganizationViewModel()
        {
            this.Organization = new Organization();
            this.Community = new List<Community>();
            this.Cookbook = new List<Cookbook>();
        }
    }
}
