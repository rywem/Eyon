using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Eyon.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }        
        public long SiteImageId { get; set; }
        [ForeignKey("SiteImageId")]
        public SiteImage SiteImage { get; set; }

        public ICollection<CookbookCategories> CookbookCategories { get; set; }
        //public ICollection<RecipeCategories> RecipeCategories { get; set; }

    }
}
