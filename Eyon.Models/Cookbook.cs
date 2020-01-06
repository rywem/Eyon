using Eyon.Models.Relationship;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eyon.Models
{
    public class Cookbook
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(300)]
        [StringLength(300)]
        public string Name { get; set; }
        [MaxLength(3000)]
        [StringLength(3000)]
        public string Description { get; set; }
        [MaxLength(200)]
        [StringLength(200)]
        public string Author { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        public string Copyright { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        public string ISBN { get; set; }        

        public ICollection<Eyon.Models.Relationship.CommunityCookbook> CommunityCookbooks { get; set; }
        public ICollection<CookbookCategories> CookbookCategories { get; set; }

        public ICollection<OrganizationCookbooks> OrganizationCookbooks { get; set; }
        public ICollection<CookbookRecipe> CookbookRecipes { get; set; }

        //public ICollection<CookbookApplicationUsers> CookbookApplicationUsers { get; set; }
        //public ICollection<CookbookSiteImages> CookbookSiteImages { get; set; }


    }
}
