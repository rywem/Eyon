using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class OrganizationViewModel
    {
        public Organization Organization { get; set; }
        public List<Community> Communities { get; set; }
        public List<Cookbook> Cookbooks { get; set; }

        public OrganizationViewModel()
        {
            this.Organization = new Organization();
            this.Communities = new List<Community>();
            this.Cookbooks = new List<Cookbook>();
        }
    }
}
