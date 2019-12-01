using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eyon.Models
{
    public class Cookbook
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Copyright { get; set; }
        public string ISBN { get; set; }        

        public ICollection<CommunityCookbooks> CommunityCookbooks { get; set; }
        public ICollection<CookbookCategories> CookbookCategories { get; set; }

        //public ICollection<OrganizationCookbooks> OrganizationCookbooks { get; set; }
        //public ICollection<CookbookRecipes> CookbookRecipes { get; set; }
        //public ICollection<CookbookSiteImages> CookbookSiteImages { get; set; }


    }
}
